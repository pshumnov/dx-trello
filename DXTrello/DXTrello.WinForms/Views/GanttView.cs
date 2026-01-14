using DXTrello.ViewModel.ViewModels;
using DXTrello.Core.Models;
using DevExpress.XtraGantt;
using DevExpress.XtraTreeList;
using DevExpress.XtraTreeList.Columns;

namespace DXTrello.WinForms {
    public partial class GanttView : DevExpress.XtraEditors.XtraUserControl {
        public GanttView() {
            InitializeComponent();
            if(!mvvmContext.IsDesignMode) {
                InitializeGantt();
                InitializeBindings();
            }
        }

        void InitializeGantt() {
            // Map the data source fields to the Gantt Control
            ganttControl1.TreeListMappings.KeyFieldName = nameof(ProjectTask.Id);
            ganttControl1.TreeListMappings.ParentFieldName = nameof(ProjectTask.ParentId);

            ganttControl1.ChartMappings.StartDateFieldName = nameof(ProjectTask.StartDate);
            ganttControl1.ChartMappings.FinishDateFieldName = nameof(ProjectTask.EndDate);
            ganttControl1.ChartMappings.ProgressFieldName = nameof(ProjectTask.ProgressPercent);
            ganttControl1.ChartMappings.TextFieldName = nameof(ProjectTask.Title);

            // Configure columns
            AddColumn(nameof(ProjectTask.Title), "Task Name", 250);
            AddColumn(nameof(ProjectTask.StartDate), "Start Date", 100);
            AddColumn(nameof(ProjectTask.EndDate), "End Date", 100);
            AddColumn(nameof(ProjectTask.ProgressPercent), "Progress", 75);

            // Allow user interaction
            ganttControl1.OptionsCustomization.AllowModifyTasks = DevExpress.Utils.DefaultBoolean.True;
            ganttControl1.OptionsBehavior.Editable = true;
        }

        void AddColumn(string fieldName, string caption, int width) {
            TreeListColumn column = ganttControl1.Columns.Add();
            column.FieldName = fieldName;
            column.Caption = caption;
            column.Width = width;
            column.Visible = true;
        }

        void InitializeBindings() {
            var fluent = mvvmContext.OfType<GanttViewModel>();
            fluent.SetBinding(ganttControl1, g => g.DataSource, x => x.Tasks);

            // Sync Selection View -> ViewModel
            fluent.WithEvent<FocusedNodeChangedEventArgs>(ganttControl1, "FocusedNodeChanged")
                .SetBinding(x => x.SelectedTask, args => ganttControl1.GetDataRecordByNode(args.Node) as ProjectTask);

            // Sync Selection ViewModel -> View 
            fluent.SetTrigger(x => x.SelectedTask, (task) => {
                if (task == null) return;
                var node = ganttControl1.FindNodeByFieldValue(nameof(ProjectTask.Id), task.Id);
                if (node != null && node != ganttControl1.FocusedNode)
                    ganttControl1.FocusedNode = node;
            });
        }
    }
}
