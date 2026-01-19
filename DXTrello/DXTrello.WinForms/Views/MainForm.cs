using DevExpress.Utils.MVVM.Services;
using DXTrello.ViewModel.Services;
using DXTrello.ViewModel.ViewModels;
using DevExpress.XtraBars;
using DXTrello.Core.Models;
using System.ComponentModel;

namespace DXTrello.WinForms {
    public partial class MainForm : DevExpress.XtraEditors.XtraForm, IToggleDetailsService, IBarProvider {
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

            // Logic to handle both Property changes (new list) and List changes (add/remove items)
            fluent.SetTrigger(x => x.Users, users => {
                // 1. Unsubscribe from old list if necessary (not critical for this scope but good practice)
                
                // 2. Initial population
                PopulateUserFilter(users);

                // 3. Subscribe to collection changes for future updates (Clear/Add)
                if (users != null)
                    users.ListChanged += (s, e) => PopulateUserFilter(users);
            });
        }

        void PopulateUserFilter(BindingList<TeamMember> users) {
             userFilterItem.ItemLinks.Clear();
             if(users == null) return;

             foreach(var user in users) {
                var checkItem = new BarCheckItem(barManager1);
                checkItem.Caption = user.DisplayName;
                checkItem.Tag = user.Id;
                checkItem.CheckedChanged += (s, e) => {
                        var vm = mvvmContext.GetViewModel<MainViewModel>();
                        vm?.ToggleUserFilter((long)checkItem.Tag);
                };
                userFilterItem.ItemLinks.Add(checkItem);
            }
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
        public Bar MainMenu => bar2;
    }
}