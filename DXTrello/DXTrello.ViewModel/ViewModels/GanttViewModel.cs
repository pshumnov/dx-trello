using DXTrello.Core.Models;
using DevExpress.Mvvm.DataAnnotations;
using System.ComponentModel;
using DevExpress.Mvvm.POCO;
using DevExpress.Mvvm;
using DXTrello.ViewModel.Messages;
using DXTrello.ViewModel.Services;

namespace DXTrello.ViewModel.ViewModels {
    [POCOViewModel()]
    public class GanttViewModel {
        protected GanttViewModel(BindingList<ProjectTask> tasks) {
            Tasks = tasks;
            Messenger.Default.Register<SelectedTaskChangedMessage>(this, OnMessageReceived);
        }
        public static GanttViewModel Create(BindingList<ProjectTask> tasks) {
            return ViewModelSource.Create(() => new GanttViewModel(tasks));
        }

        public virtual IGanttViewService GanttViewService => this.GetService<IGanttViewService>();

        public virtual BindingList<ProjectTask> Tasks { get; protected set; }
        public virtual ProjectTask? SelectedTask { get; set; }
        public virtual TimescaleEnum Timescale { get; set; } = TimescaleEnum.Week;
        public virtual bool TaskTableVisibility { get; set; } = true;
        public virtual bool TimelineVisibility { get; set; } = false;

        #region Commands
        public void ScrollToToday() {
            GanttViewService.ScrollToDate(DateTime.Today);
        }
        public void ToggleTimeline() {
            TimelineVisibility = !TimelineVisibility;
        }
        public void ToggleTaskTable() {
            TaskTableVisibility = !TaskTableVisibility;
        }
        public void OpenDetailsPanel() {
            if(SelectedTask == null) {
                SelectedTask = GanttViewService.GetFocusedNodeTask();
            }
            Messenger.Default.Send(new ToggleDetailsWindowMessage(true));
        }
        public void CloseDetailsPanel() {
            Messenger.Default.Send(new ToggleDetailsWindowMessage(false));
        }
        #endregion

        #region Change Handlers
        protected void OnTimescaleChanged() {
            GanttViewService.ApplyTimescale(Timescale, SelectedTask?.StartDate ?? DateTime.Today);
        }
        protected void OnSelectedTaskChanged() {
            Messenger.Default.Send(new SelectedTaskChangedMessage(SelectedTask));

        }
        #endregion
        #region Events
        public void ProcessKey(int key) {
            if(key == 46 && SelectedTask != null) {
                Tasks.Remove(SelectedTask);
                SelectedTask = null;
            }
            if(key == 27) {
                CloseDetailsPanel();
            }
        }
        public async Task OnViewLoad() {
            GanttViewService.MergeBar();
        }
        #endregion
        void OnMessageReceived(SelectedTaskChangedMessage message) {
            if(SelectedTask != message.SelectedTask)
                SelectedTask = message.SelectedTask;
        }
    }
    public enum TimescaleEnum {
        Day,
        Week,
        Month,
        Quarter
    }
}
