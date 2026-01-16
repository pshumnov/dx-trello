using DevExpress.Mvvm;
using DevExpress.Utils.MVVM.Services;
using DevExpress.XtraBars.Docking;
using DXTrello.ViewModel.Services;
using DXTrello.ViewModel.ViewModels;

namespace DXTrello.WinForms {
    public partial class MainForm : DevExpress.XtraEditors.XtraForm, IToggleDetailsService {
        public MainForm() {
            InitializeComponent();

            if(!mvvmContext.IsDesignMode) {
                InitializeBindings();
                RegisterServices();
            }

            detailsPanel.Width = 500;
        }
        void InitializeBindings() {
            var fluent = mvvmContext.OfType<MainViewModel>();
            fluent.WithEvent(this, "Load")
                .EventToCommand(x => x.OnViewLoad);
            fluent.SetBinding(this, f => f.Text, x => x.Title);
        }
        void RegisterServices() {
            mvvmContext.RegisterService("TabbedView", DocumentManagerService.Create(tabbedView1));
            mvvmContext.RegisterService(this);
        }
        public void ShowDetails(bool show) {
            if(show)
                detailsPanel.ShowPopup();
            else
                detailsPanel.HidePopup();
        }
    }
}