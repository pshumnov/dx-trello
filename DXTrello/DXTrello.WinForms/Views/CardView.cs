using DXTrello.ViewModel.ViewModels;

namespace DXTrello.WinForms {
    public partial class CardView : DevExpress.XtraEditors.XtraUserControl {
        public CardView() {
            InitializeComponent();
        }
        public void SetViewModel(CardViewModel viewModel) {
            mvvmContext.SetViewModel(typeof(CardViewModel), viewModel);
            InitializeBindings();
        }
        void InitializeBindings() {
            var fluent = mvvmContext.OfType<CardViewModel>();
            fluent.SetBinding(gridControl1, g => g.DataSource, x => x.Tasks);
        }
    }
}
