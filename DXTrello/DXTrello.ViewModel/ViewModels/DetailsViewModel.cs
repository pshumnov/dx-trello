using DevExpress.Mvvm;
using DevExpress.Mvvm.DataAnnotations;
using DevExpress.Mvvm.POCO;
using DXTrello.Core.Models;
using DXTrello.Core.Services;
using DXTrello.ViewModel.Messages;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;
using System.Windows.Input;

namespace DXTrello.ViewModel.ViewModels {
    [POCOViewModel()]
    public class DetailsViewModel {
        public DetailsViewModel() {
            Messenger.Default.Register<SelectedTaskChangedMessage>(this, OnMessageReceived);
        }
        protected DetailsViewModel(ProjectTask? focusedTask) {
            Task = focusedTask;
        }
        public static DetailsViewModel Create(ProjectTask? focusedTask) {
            return ViewModelSource.Create(() => new DetailsViewModel(focusedTask));
        }
        public static DetailsViewModel Create() {
            return ViewModelSource.Create(() => new DetailsViewModel());
        }
        void OnMessageReceived(SelectedTaskChangedMessage message) {
            if(Task != message.SelectedTask)
                Task = message.SelectedTask;
        }
        public virtual ProjectTask? Task { get; set; }
        public virtual ITaskDataService TaskDataService => this.GetService<ITaskDataService>();
        public virtual IList<TeamMember> Users => TaskDataService.GetTeamMembersAsync().Result;
        public event EventHandler? FormValidityChanged;
        bool isFormValid;
        public virtual bool IsFormValid {
            get => isFormValid;
            set {
                isFormValid = value;
                FormValidityChanged?.Invoke(this, EventArgs.Empty);
            }
        }
    }
}
