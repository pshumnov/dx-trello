using DevExpress.XtraBars.Docking2010.Customization;
using DevExpress.XtraDialogs.Internal;
using DevExpress.XtraEditors;
using DevExpress.XtraEditors.Base;
using DevExpress.XtraEditors.Controls;
using DevExpress.XtraGrid.Views.Grid;
using DXTrello.Core.Models;
using DXTrello.Core.Services;
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
    public partial class DetailsView : XtraUserControl, INotifyPropertyChanged {
        public DetailsView() {
            InitializeComponent();
            if(!mvvmContext.IsDesignMode) {
                InitializeControls();
                InitializeBindings();
                ValidateForm();
            }
        }

        bool isTitleEditValid = true;
        bool isStartDateEditValid = true;
        bool isEndDateEditValid = true;

        public event PropertyChangedEventHandler? PropertyChanged;

        private bool isFormValid;
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public bool IsFormValid {
            get => isFormValid;
            protected set {
                if(isFormValid == value)
                    return;
                isFormValid = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(IsFormValid)));
            }
        }

        void InitializeControls() {
            startDateEdit.Properties.NullDate = DateTime.MinValue;
            startDateEdit.Properties.NullDateCalendarValue = DateTime.Today;
            endDateEdit.Properties.NullDate = DateTime.MinValue;
            userSelection.Properties.KeyMember = nameof(TeamMember.Id);
            userSelection.Properties.AllowNullInput = DevExpress.Utils.DefaultBoolean.True;

            userSelection.Properties.Columns.Add(new LookUpColumnInfo(nameof(TeamMember.Login), "Login"));
            userSelection.Properties.Columns.Add(new LookUpColumnInfo(nameof(TeamMember.DisplayName), "Name"));
            userSelection.Properties.Columns.Add(new LookUpColumnInfo(nameof(TeamMember.Email), "Email"));

            userSelection.Properties.NullText = "Not assigned";
            titleEdit.Properties.NullValuePrompt = "[Enter task title]";
            startDateEdit.Properties.NullValuePrompt = "[Select start date]";

            titleEdit.Validating += (s, e) => {
                if(!IsTitleValid()) {
                    e.Cancel = true;
                    isTitleEditValid = false;
                    UpdateFormValidity();
                    titleEdit.ErrorText = "Title cannot be empty.";
                }
            };
            titleEdit.Validated += (s, e) => {
                isTitleEditValid = true;
                UpdateFormValidity();
            };
            startDateEdit.Validating += (s, e) => {
                if(!IsStartDateValid(out string error)) {
                    e.Cancel = true;
                    isStartDateEditValid = false;
                    UpdateFormValidity();
                    startDateEdit.ErrorText = error;
                }
            };
            startDateEdit.Validated += (s, e) => {
                isStartDateEditValid = true;
                UpdateFormValidity();
            };
            endDateEdit.Validating += (s, e) => {
                if(!IsEndDateValid()) {
                    e.Cancel = true;
                    isEndDateEditValid = false;
                    UpdateFormValidity();
                    endDateEdit.ErrorText = "End Date cannot be earlier than Start Date.";
                }
            };
            endDateEdit.Validated += (s, e) => {
                isEndDateEditValid = true;
                UpdateFormValidity();
            };
        }

        void InitializeBindings() {
            var fluent = mvvmContext.OfType<DetailsViewModel>();
            mvvmContext.RegisterService(new SampleTaskDataService());

            fluent.SetBinding(titleEdit, t => t.EditValue, vm => vm.Task.Title);
            fluent.SetBinding(descriptionEdit, t => t.EditValue, vm => vm.Task.Description);
            fluent.SetBinding(startDateEdit, t => t.EditValue, vm => vm.Task.StartDate);
            fluent.SetBinding(endDateEdit, t => t.EditValue, vm => vm.Task.EndDate);
            fluent.SetBinding(userSelection, us => us.EditValue, vm => vm.Task.Assignee);

            fluent.SetBinding(userSelection.Properties, usp => usp.DataSource, vm => vm.Users);

            fluent.SetBinding(this, f => f.IsFormValid, vm => vm.IsFormValid);
        }

        void ValidateForm() {
            isTitleEditValid = IsTitleValid();
            isStartDateEditValid = IsStartDateValid(out _);
            isEndDateEditValid = IsEndDateValid();
        }

        bool IsTitleValid() {
            return !string.IsNullOrWhiteSpace(titleEdit.Text);
        }

        bool IsStartDateValid(out string error) {
            error = string.Empty;
            if(startDateEdit.DateTime == DateTime.MinValue) {
                error = "Start Date cannot be empty.";
                return false;
            } else if(endDateEdit.DateTime != DateTime.MinValue && startDateEdit.DateTime > endDateEdit.DateTime) {
                error = "Start Date cannot be later than End Date.";
                return false;
            }
            return true;
        }

        bool IsEndDateValid() {
            return endDateEdit.DateTime == DateTime.MinValue ||
                   (startDateEdit.DateTime != DateTime.MinValue && endDateEdit.DateTime >= startDateEdit.DateTime);
        }

        void UpdateFormValidity() {
            IsFormValid = isTitleEditValid && isStartDateEditValid && isEndDateEditValid;
        }
    }
}
