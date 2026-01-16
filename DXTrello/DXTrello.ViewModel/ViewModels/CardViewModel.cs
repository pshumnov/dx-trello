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
        public virtual IDialogService DialogService => this.GetService<IDialogService>();

        protected void OnSelectedTaskChanged() {
            Messenger.Default.Send(new SelectedTaskChangedMessage(SelectedTask));
            if(SelectedTask == null)
                CloseDetailsPanel();
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
        public void ProcessKey(int key) {
            if(key == 46 && SelectedTask != null) {
                Tasks.Remove(SelectedTask);
                SelectedTask = null;
            }
        }
        public void OpenDetailsPanel() {
            Messenger.Default.Send(new ToggleDetailsWindowMessage(true));
        }
        public void CloseDetailsPanel() {
            Messenger.Default.Send(new ToggleDetailsWindowMessage(false));
        }
        void CreateAndFocusNewTask(ProjectTaskStatus targetStatus, bool prepend) {
            ProjectTask newTask = new ProjectTask() {
                Status = targetStatus,
                Id = Tasks.Count + 1,
                ParentId = -1
            };
            DetailsViewModel detailsViewModel = DetailsViewModel.Create(newTask);
            var command = new DelegateCommand(() => { },
                () => detailsViewModel.IsFormValid);
            EventHandler? formValidityChangedHandler = (s, e) => {
                command.RaiseCanExecuteChanged();
            };
            try {
                detailsViewModel.FormValidityChanged += formValidityChangedHandler;
                List<UICommand> buttons = new List<UICommand>() {
                new UICommand {
                    Id = "Create",
                    Caption = "Create",
                    Tag = MessageResult.Yes,
                    Command = command
                },
                new UICommand {
                    Id = "Cancel",
                    Caption = "Cancel",
                    Tag = MessageResult.Cancel
                } };
                var result = DialogService.ShowDialog(buttons, "Create task", detailsViewModel);
                if(result.Tag is MessageResult.Yes) {
                    int targetIdx = prepend ? 0 : Tasks.Count;
                    Tasks.Insert(targetIdx, newTask);
                    SelectedTask = newTask;
                }
            }
            finally {
                detailsViewModel.FormValidityChanged -= formValidityChangedHandler;
            }
        }
    }
}
