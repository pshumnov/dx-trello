using DXTrello.ViewModel.ViewModels;

namespace DXTrello.WinForms {
    public partial class MainForm : DevExpress.XtraEditors.XtraForm {
        public MainForm() {
            InitializeComponent();
            if(!mvvmContext.IsDesignMode)
                InitializeBindings();
        }
        void InitializeBindings() {
            var fluent = mvvmContext.OfType<MainViewModel>();
            cardView.SetViewModel(fluent.ViewModel.CardViewModel);
            ganttView.SetViewModel(fluent.ViewModel.GanttViewModel);
            fluent.WithEvent(this, "Load")
                .EventToCommand(x => x.OnViewLoad);
            fluent.SetBinding(this, f => f.Text, x => x.Title);
        }
    }
}