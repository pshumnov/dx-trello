using DXTrello.Core.Models;
using DevExpress.Mvvm.DataAnnotations;
using System.ComponentModel;
using DevExpress.Mvvm.POCO;
using DevExpress.Mvvm;
using DXTrello.ViewModel.Messages;

namespace DXTrello.ViewModel.ViewModels {
    [POCOViewModel()]
    public class CardViewModel {
        protected CardViewModel(BindingList<ProjectTask> tasks) {
            Tasks = tasks;
            Messenger.Default.Register<SelectedTaskChangedMessage>(this, OnMessageReceived);
        }
        public static CardViewModel Create(BindingList<ProjectTask> tasks) {
            return ViewModelSource.Create(() => new CardViewModel(tasks));
        }
        public virtual BindingList<ProjectTask> Tasks { get; protected set; }
        public virtual ProjectTask? SelectedTask { get; set; }

        protected void OnSelectedTaskChanged() {
            Messenger.Default.Send(new SelectedTaskChangedMessage(SelectedTask));
        }
        void OnMessageReceived(SelectedTaskChangedMessage message) {
            if(SelectedTask != message.SelectedTask)
                SelectedTask = message.SelectedTask;
        }
    }
}
