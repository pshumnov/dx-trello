using DXTrello.ViewModel.ViewModels;

namespace DXTrello.WinForms {
    public partial class CardView : DevExpress.XtraEditors.XtraUserControl {
        public CardView() {
            InitializeComponent();
            InitializeBindings();
        }
        void InitializeBindings() {
            var fluent = mvvmContext.OfType<CardViewModel>();
            fluent.SetBinding(gridControl1, g => g.DataSource, x => x.Tasks);
        }
    }
}
