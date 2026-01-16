using DXTrello.ViewModel.ViewModels;
using DXTrello.Core.Models;
using DevExpress.XtraGantt;
using DevExpress.XtraTreeList;
using DevExpress.XtraTreeList.Columns;
using DevExpress.XtraBars;
using DXTrello.ViewModel.Services;
using DevExpress.LookAndFeel;

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
            // Map the data source fields to the Gantt Control
            ganttControl.TreeListMappings.KeyFieldName = nameof(ProjectTask.Id);
            ganttControl.TreeListMappings.ParentFieldName = nameof(ProjectTask.ParentId);

            ganttControl.ChartMappings.StartDateFieldName = nameof(ProjectTask.StartDate);
            ganttControl.ChartMappings.FinishDateFieldName = nameof(ProjectTask.EndDate);
            ganttControl.ChartMappings.ProgressFieldName = nameof(ProjectTask.ProgressPercent);
            ganttControl.ChartMappings.TextFieldName = nameof(ProjectTask.Title);

            // Configure columns
            AddColumn(nameof(ProjectTask.Title), "Task Name", 250);

            // Allow user interaction
            ganttControl.OptionsCustomization.AllowModifyTasks = DevExpress.Utils.DefaultBoolean.True;
            ganttControl.OptionsBehavior.Editable = true;
            //
            ganttControl.ZoomMode = GanttZoomMode.Smooth;
            ganttControl.ChartStartDate = null;
            ganttControl.ChartFinishDate = null;

            ganttControl.CustomDrawTask += (s, e) => {
                if(e.Info is DevExpress.XtraGantt.Chart.GanttChartSummaryTaskInfo) {
                    DevExpress.XtraGantt.Chart.GanttChartSummaryTaskInfo summaryTaskInfo = (DevExpress.XtraGantt.Chart.GanttChartSummaryTaskInfo)e.Info;
                    if(summaryTaskInfo.Type == DevExpress.XtraGantt.Chart.GanttChartItemType.SummaryTask) {
                        e.Cache.FillRectangle(DXSkinColors.FillColors.Primary, e.Info.VisibleShapeBounds);
                        e.Handled = true;
                    }
                }
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

                    var previousSmoothingMode = e.Cache.SmoothingMode;
                    e.Cache.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.AntiAlias;

                    e.Cache.FillRectangle(DXSkinColors.FillColors.Danger, lineRect);
                    e.Cache.FillEllipse(DXSkinColors.FillColors.Danger, pointRect);
                    e.Cache.DrawRectangle(textRect.X, textRect.Y, textRect.Width, textRect.Height, DXSkinColors.FillColors.Danger, textRectThickness);

                    e.Cache.SmoothingMode = previousSmoothingMode;
                    e.Handled = true;
                }
            };
        }

        void AddColumn(string fieldName, string caption, int width) {
            TreeListColumn column = ganttControl.Columns.Add();
            column.FieldName = fieldName;
            column.Caption = caption;
            column.Width = width;
            column.Visible = true;
        }

        void InitializeBindings() {
            var fluent = mvvmContext.OfType<GanttViewModel>();
            fluent.SetBinding(ganttControl, g => g.DataSource, x => x.Tasks);

            // Sync Selection View -> ViewModel
            fluent.WithEvent<FocusedNodeChangedEventArgs>(ganttControl, "FocusedNodeChanged")
                .SetBinding(vm => vm.SelectedTask, args => ganttControl.GetDataRecordByNode(args.Node) as ProjectTask);
            fluent.WithEvent<MouseEventArgs>(ganttControl, "MouseDown")
                .EventToCommand(vm => vm.ClearSelection(), ShouldClickClearSelection);

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
        }

        bool ShouldClickClearSelection(MouseEventArgs args) {
            var hitInfo = ganttControl.CalcHitInfo(args.Location);
            if(hitInfo.InChart && hitInfo.ChartHitTest.RowInfo != null)
                return false;
            if(hitInfo.InTreeList && hitInfo.TreeListHitTest.InRow)
                return false;
            return true;
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
    }
}
