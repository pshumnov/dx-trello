using DXTrello.ViewModel.ViewModels;

namespace DXTrello.WinForms {
    public partial class GanttView : DevExpress.XtraEditors.XtraUserControl {
        public GanttView() {
            InitializeComponent();
        }
        public void SetViewModel(GanttViewModel viewModel) {
            mvvmContext.SetViewModel(typeof(GanttViewModel), viewModel);
            InitializeBindings();
        }
        void InitializeBindings() {
            var fluent = mvvmContext.OfType<GanttViewModel>();
            fluent.SetBinding(ganttControl1, g => g.DataSource, x => x.Tasks);
        }
    }
}
