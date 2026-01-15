using DevExpress.XtraBars.Docking2010.Customization;
using DevExpress.XtraDialogs.Internal;
using DevExpress.XtraEditors;
using DevExpress.XtraEditors.Base;
using DevExpress.XtraGrid.Views.Grid;
using DXTrello.Core.Models;
using DXTrello.ViewModel.ViewModels;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace DXTrello.WinForms {
    public partial class DetailsView : XtraUserControl {
        public DetailsView() {
            InitializeComponent();
            if(!mvvmContext.IsDesignMode) {
                InitializeControls();
                InitializeBindings();
            }
        }

        void InitializeControls() {
            startDateEdit.Properties.NullDate = DateTime.MinValue;
            startDateEdit.Properties.NullDateCalendarValue = DateTime.Today;
            endDateEdit.Properties.NullDate = DateTime.MinValue;
        }

        void InitializeBindings() {
            var fluent = mvvmContext.OfType<DetailsViewModel>();

            fluent.SetBinding(titleEdit, t => t.EditValue, vm => vm.Task.Title);
            fluent.SetBinding(descriptionEdit, t => t.EditValue, vm => vm.Task.Description);
            fluent.SetBinding(startDateEdit, t => t.EditValue, vm => vm.Task.StartDate);
            fluent.SetBinding(endDateEdit, t => t.EditValue, vm => vm.Task.EndDate);
        }
    }
}
