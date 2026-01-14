using DevExpress.Mvvm;
using DevExpress.Utils.MVVM.Services;
using DXTrello.ViewModel.ViewModels;

namespace DXTrello.WinForms {
    public partial class MainForm : DevExpress.XtraEditors.XtraForm {
        public MainForm() {
            InitializeComponent();

            if(!mvvmContext.IsDesignMode) {
                InitializeBindings();
                RegisterServices();
            }
        }
        void InitializeBindings() {
            var fluent = mvvmContext.OfType<MainViewModel>();
            fluent.WithEvent(this, "Load")
                .EventToCommand(x => x.OnViewLoad);
            fluent.SetBinding(this, f => f.Text, x => x.Title);
        }
        void RegisterServices() {
            mvvmContext.RegisterService(DocumentManagerService.Create(tabbedView1));
        }
    }
}