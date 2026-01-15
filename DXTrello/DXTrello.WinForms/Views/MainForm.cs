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

            dockManager1.RegisterDockPanel += RegisterDockPanel;
        }
        void RegisterDockPanel(object sender, DockPanelEventArgs e) {
            e.Panel.DockTo(DockingStyle.Right);
            e.Panel.Visibility = DockVisibility.Hidden;
            e.Panel.Options.ShowAutoHideButton = false;
            e.Panel.Options.ShowCloseButton = false;
        }
        void InitializeBindings() {
            var fluent = mvvmContext.OfType<MainViewModel>();
            fluent.WithEvent(this, "Load")
                .EventToCommand(x => x.OnViewLoad);
            fluent.SetBinding(this, f => f.Text, x => x.Title);
        }
        void RegisterServices() {
            mvvmContext.RegisterService("TabbedView", DocumentManagerService.Create(tabbedView1));
            mvvmContext.RegisterService("DockManager", DocumentManagerService.Create(dockManager1));
            mvvmContext.RegisterService(this);
        }
        public void ShowDetails(bool show) {
            var targetPanel = dockManager1.Panels[0];
            targetPanel.Visibility = show ? DockVisibility.Visible : DockVisibility.Hidden;
            targetPanel.Width = 450;
        }
    }
}