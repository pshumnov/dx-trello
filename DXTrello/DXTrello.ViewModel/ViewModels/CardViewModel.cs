using DXTrello.Core.Models;
using DevExpress.Mvvm.DataAnnotations;
using System.ComponentModel;
using DevExpress.Mvvm.POCO;
using DevExpress.Mvvm;
using DXTrello.ViewModel.Messages;
using DXTrello.Core.Enums;
using DXTrello.ViewModel.Services;

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
        public virtual ICardViewService CardViewService => this.GetService<ICardViewService>();

        protected void OnSelectedTaskChanged() {
            Messenger.Default.Send(new SelectedTaskChangedMessage(SelectedTask));
        }
        void OnMessageReceived(SelectedTaskChangedMessage message) {
            if(SelectedTask != message.SelectedTask)
                SelectedTask = message.SelectedTask;
        }
        public void AppendCreateNewTask(ProjectTaskStatus targetStatus) {
            CreateAndFocusNewTask(targetStatus, false);
        }
        public void PrependCreateNewTask(ProjectTaskStatus targetStatus) {
            CreateAndFocusNewTask(targetStatus, true);
        }
        void CreateAndFocusNewTask(ProjectTaskStatus targetStatus, bool prepend) {
            ProjectTask newTask = new ProjectTask() {
                Status = targetStatus,
                Id = Tasks.Count + 1,
                ParentId = -1
            };
            int index = prepend ? 0 : Tasks.Count;
            Tasks.Insert(index, newTask);
            SelectedTask = newTask;
            CardViewService?.ShowEditForm();
        }
    }
}
