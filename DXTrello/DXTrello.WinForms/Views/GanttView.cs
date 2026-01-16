using DXTrello.ViewModel.ViewModels;
using DXTrello.Core.Models;
using DevExpress.XtraGantt;
using DevExpress.XtraTreeList;
using DevExpress.XtraTreeList.Columns;
using DevExpress.XtraBars;
using DXTrello.ViewModel.Services;
using DevExpress.LookAndFeel;
using DevExpress.Utils;
using DevExpress.Utils.Extensions;
using DevExpress.Utils.Drawing;
using System.Drawing.Drawing2D;

namespace DXTrello.WinForms {
    public partial class GanttView : DevExpress.XtraEditors.XtraUserControl, IGanttViewService {
        public GanttView() {
            InitializeComponent();
            if(!mvvmContext.IsDesignMode) {
                InitializeGantt();
                InitializeBindings();
                RegisterServices();
            }
        }

        void InitializeGantt() {
            ganttControl.OptionsBehavior.ScheduleMode = DevExpress.XtraGantt.Options.ScheduleMode.Manual;

            // Map the data source fields to the Gantt Control
            ganttControl.TreeListMappings.KeyFieldName = nameof(ProjectTask.Id);
            ganttControl.TreeListMappings.ParentFieldName = nameof(ProjectTask.ParentId);

            ganttControl.ChartMappings.StartDateFieldName = nameof(ProjectTask.StartDate);
            ganttControl.ChartMappings.FinishDateFieldName = nameof(ProjectTask.EndDate);
            ganttControl.ChartMappings.TextFieldName = nameof(ProjectTask.Title);

            // Configure columns
            AddColumn(nameof(ProjectTask.Title), "Task Name", 250, false);

            // Allow user interaction
            ganttControl.OptionsCustomization.AllowModifyTasks = DefaultBoolean.True;
            ganttControl.OptionsCustomization.AllowModifyProgress = DefaultBoolean.False;
            ganttControl.OptionsBehavior.Editable = true;
            //
            ganttControl.ZoomMode = GanttZoomMode.Smooth;
            ganttControl.ChartStartDate = null;
            ganttControl.ChartFinishDate = null;

            ganttControl.CustomDrawTask += (s, e) => {
                const int cornerR = 4, taskHeight = 35;
                var color = DXSkinColors.FillColors.Primary;
                //if(e.Info.Type == DevExpress.XtraGantt.Chart.GanttChartItemType.SummaryTask)
                //    color = DXSkinColors.FillColors.Question;
                var rect = e.Info.VisibleShapeBounds.ToRectangle();
                rect.Inflate(0, Math.Max(0, taskHeight - rect.Height) / 2);
                var cornerRadius = new CornerRadius(cornerR);

                e.Cache.FillRoundedRectangle(color, rect, cornerRadius);
                DrawWithSmoothing(e.Cache, () => {
                    e.Cache.DrawRoundedRectangle(color, 1, rect, cornerRadius);
                });
                
                e.DrawRightText();
                e.Handled = true;
            };
            ganttControl.CustomDrawTimescaleColumn += (s, e) => {
                const float lineWidth = 1f, pointRadius = 4f, textInflate = 4f;
                const int textRectThickness = 2;
                DateTime today = DateTime.Now;

                if(today >= e.Column.StartDate && today < e.Column.FinishDate && e.Column.Bounds.Y > 0) {
                    e.DefaultDraw();
                    var lineStart = new PointF((float)e.GetPosition(today), e.Column.VisibleHeaderBounds.Bottom);
                    var lineRect = new RectangleF(lineStart, new SizeF(lineWidth, e.Column.Bounds.Height));
                    var pointRect = new RectangleF(lineStart.X - pointRadius, lineStart.Y - pointRadius, pointRadius * 2, pointRadius * 2);
                    var textRect = e.Column.TextBounds;
                    textRect.Inflate(textInflate, textInflate);

                    DrawWithSmoothing(e.Cache, () => {
                        e.Cache.FillRectangle(DXSkinColors.FillColors.Danger, lineRect);
                        e.Cache.FillEllipse(DXSkinColors.FillColors.Danger, pointRect);
                        e.Cache.DrawRectangle(textRect.X, textRect.Y, textRect.Width, textRect.Height, DXSkinColors.FillColors.Danger, textRectThickness);

                    });

                    e.Handled = true;
                }
            };
        }
        void DrawWithSmoothing(GraphicsCache cache, Action draw, SmoothingMode mode = SmoothingMode.AntiAlias) {
            var previousSmoothingMode = cache.SmoothingMode;
            cache.SmoothingMode = mode;
            try {
                draw();
            }
            finally {
                cache.SmoothingMode = previousSmoothingMode;
            }
        }

        void AddColumn(string fieldName, string caption, int width, bool editable = true) {
            TreeListColumn column = ganttControl.Columns.Add();
            column.FieldName = fieldName;
            column.Caption = caption;
            column.Width = width;
            column.Visible = true;
            column.OptionsColumn.AllowEdit = editable;
        }

        void InitializeBindings() {
            var fluent = mvvmContext.OfType<GanttViewModel>();
            fluent.SetBinding(ganttControl, g => g.DataSource, x => x.Tasks);

            // Sync Selection View -> ViewModel
            fluent.WithEvent<FocusedNodeChangedEventArgs>(ganttControl, "FocusedNodeChanged")
                .SetBinding(vm => vm.SelectedTask, args => ganttControl.GetDataRecordByNode(args.Node) as ProjectTask);
            fluent.WithEvent<MouseEventArgs>(ganttControl, "MouseDown")
                .EventToCommand(vm => vm.CloseDetailsPanel, ShouldClickClosePanel);

            // Sync Selection ViewModel -> View 
            fluent.SetTrigger(x => x.SelectedTask, (task) => {
                if(task == null) {
                    ganttControl.ClearFocusedColumn();
                    return;
                }
                var node = ganttControl.FindNodeByFieldValue(nameof(ProjectTask.Id), task.Id);
                if(node != null && node != ganttControl.FocusedNode)
                    ganttControl.FocusedNode = node;
            });

            // Buttons
            fluent.BindCommand(toggleTreeItem, vm => vm.ToggleTaskTable);
            fluent.BindCommand(toggleTimelineItem, vm => vm.ToggleTimeline);
            fluent.BindCommand(todayItem, vm => vm.ScrollToToday);

            // State Binding
            fluent.SetTrigger(vm => vm.TaskTableVisibility, SetTaskTableVisibility);
            fluent.SetTrigger(vm => vm.TimelineVisibility, SetTimelineVisibility);

            // Time Period ListBox
            fluent.WithEvent<ListItemClickEventArgs>(timescaleItem, "ListItemClick")
                .SetBinding(vm => vm.Timescale, args => Enum.Parse<TimescaleEnum>(timescaleItem.Strings[args.Index]));
            fluent.SetBinding(timescaleItem, item => item.Caption, vm => vm.Timescale);

            // Events
            fluent.WithEvent<KeyEventArgs>(ganttControl, "KeyUp")
                .EventToCommand(
                    x => x.ProcessKey,
                    p => p.KeyValue
                );
            fluent.WithEvent<MouseEventArgs>(ganttControl, "MouseDoubleClick")
                .EventToCommand(
                    x => x.OpenDetailsPanel,
                    ShouldDoubleClickOpenPanel
                );
        }

        bool ShouldClickClosePanel(MouseEventArgs args) {
            var hitInfo = ganttControl.CalcHitInfo(args.Location);
            if(hitInfo.InChart && hitInfo.ChartHitTest.RowInfo != null)
                return false;
            if(hitInfo.InTreeList && hitInfo.TreeListHitTest.InRow)
                return false;
            return true;
        }

        bool ShouldDoubleClickOpenPanel(MouseEventArgs args) {
            var hitInfo = ganttControl.CalcHitInfo(args.Location);
            if(hitInfo.InTreeList && hitInfo.TreeListHitTest.InRow)
                return true;
            return false;
        }

        void RegisterServices() {
            mvvmContext.RegisterService(this);
        }

        void SetTaskTableVisibility(bool visible) {
            ganttControl.ClearSelection();
            ganttControl.OptionsSplitter.PanelVisibility = visible ? GanttPanelVisibility.Both : GanttPanelVisibility.Chart;
        }
        void SetTimelineVisibility(bool visible) {
            ganttControl.OptionsTimeline.TimelinePosition = visible ? TimelinePosition.Bottom : TimelinePosition.None;
        }

        void IGanttViewService.ScrollToDate(DateTime date) {
            ganttControl.ScrollChartToDate(date);
        }
        void IGanttViewService.ApplyTimescale(TimescaleEnum timescale, DateTime startDate) {
            DateTime finishDate = startDate;
            switch(timescale) {
                case TimescaleEnum.Day:
                    ganttControl.OptionsMainTimeRuler.Unit = GanttTimescaleUnit.Hour;
                    finishDate = startDate.AddDays(1);
                    break;
                case TimescaleEnum.Week:
                    ganttControl.OptionsMainTimeRuler.Unit = GanttTimescaleUnit.Day;
                    finishDate = startDate.AddDays(7);
                    break;
                case TimescaleEnum.Month:
                    ganttControl.OptionsMainTimeRuler.Unit = GanttTimescaleUnit.Week;
                    finishDate = startDate.AddMonths(1);
                    break;
                case TimescaleEnum.Quarter:
                    ganttControl.OptionsMainTimeRuler.Unit = GanttTimescaleUnit.Month;
                    finishDate = startDate.AddMonths(4);
                    break;
            }
            ganttControl.SetChartVisibleRange(startDate, finishDate);
            ganttControl.SetTimelineVisibleRange(startDate, finishDate);
        }
        ProjectTask? IGanttViewService.GetFocusedNodeTask() {
            return ganttControl.GetDataRecordByNode(ganttControl.FocusedNode) as ProjectTask;
        }
    }
}
