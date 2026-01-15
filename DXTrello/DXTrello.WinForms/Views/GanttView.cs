using DXTrello.ViewModel.ViewModels;
using DXTrello.Core.Models;
using DevExpress.XtraGantt;
using DevExpress.XtraTreeList;
using DevExpress.XtraTreeList.Columns;
using DevExpress.XtraBars;
using DXTrello.ViewModel.Services;
using DevExpress.XtraRichEdit.Layout.Engine;
using DevExpress.XtraRichEdit.Commands.Internal;

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
            // AddColumn(nameof(ProjectTask.ProgressPercent), "Progress", 75);

            // Allow user interaction
            ganttControl.OptionsCustomization.AllowModifyTasks = DevExpress.Utils.DefaultBoolean.True;
            ganttControl.OptionsBehavior.Editable = true;
            //
            ganttControl.ZoomMode = GanttZoomMode.Smooth;
            ganttControl.ChartStartDate = null;
            ganttControl.ChartFinishDate = null;
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

            // Sync Selection ViewModel -> View 
            fluent.SetTrigger(x => x.SelectedTask, (task) => {
                if (task == null) return;
                var node = ganttControl.FindNodeByFieldValue(nameof(ProjectTask.Id), task.Id);
                if (node != null && node != ganttControl.FocusedNode)
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

        void RegisterServices() {
            mvvmContext.RegisterService(this);
        }

        void SetTaskTableVisibility(bool visible) {
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
