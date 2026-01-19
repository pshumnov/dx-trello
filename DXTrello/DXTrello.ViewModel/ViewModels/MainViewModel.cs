using DXTrello.Core.Models;
using DXTrello.Core.Services;
using DevExpress.Mvvm.DataAnnotations;
using System.ComponentModel;
using DevExpress.Mvvm.POCO;
using DevExpress.Mvvm;
using DXTrello.ViewModel.Services;
using DXTrello.ViewModel.Messages;
using System.Linq;
using System.Collections.Generic;

namespace DXTrello.ViewModel.ViewModels {
    [POCOViewModel()]
    public class MainViewModel {
        private readonly ITaskDataService taskDataService;
        private IList<ProjectTask> allTasks; // Cache for all tasks to support filtering

        public MainViewModel() : this(new SampleTaskDataService()) {
        }

        public MainViewModel(ITaskDataService taskDataService) {
            this.taskDataService = taskDataService ?? throw new ArgumentNullException(nameof(taskDataService));
            Title = "DXTrello";
            Tasks = [];
            allTasks = [];
            Users = [];
            SelectedUserIds = new HashSet<long>();
            SelectedViewIndex = 0;
            Messenger.Default.Register<ToggleDetailsWindowMessage>(this, OnToggleMessageReceived);

            CardViewModel = CardViewModel.Create(Tasks).SetParentViewModel(this);
            GanttViewModel = GanttViewModel.Create(Tasks).SetParentViewModel(this);
            DetailsViewModel = DetailsViewModel.Create().SetParentViewModel(this);
        }
        
        public virtual CardViewModel CardViewModel { get; protected set; }
        public virtual GanttViewModel GanttViewModel { get; protected set; }
        public virtual DetailsViewModel DetailsViewModel { get; protected set; }
        public string Title { get; }
        public virtual BindingList<ProjectTask> Tasks { get; protected set; }
        public virtual BindingList<TeamMember> Users { get; protected set; }
        public virtual HashSet<long> SelectedUserIds { get; protected set; }

        public virtual int SelectedViewIndex { get; set; }
        public virtual IToggleDetailsService ToggleDetailsService => this.GetService<IToggleDetailsService>();

        public async Task OnViewLoad() {
            await LoadUsers();
            await LoadProjectTasks();
            InitDocuments();
        }
        public async Task LoadProjectTasks() {
            allTasks = await taskDataService.GetProjectTasksAsync().ConfigureAwait(false);
            ApplyFilter();
        }
        public async Task LoadUsers() {
            var users = await taskDataService.GetTeamMembersAsync().ConfigureAwait(false);
            Users = new BindingList<TeamMember>(users);
            CardViewModel.SetUsers(users);
        }
        public void ToggleUserFilter(long userId) {
            if(SelectedUserIds.Contains(userId))
                SelectedUserIds.Remove(userId);
            else
                SelectedUserIds.Add(userId);
            ApplyFilter();
        }
        void ApplyFilter() {
            Tasks.RaiseListChangedEvents = false;
            Tasks.Clear();
            foreach(var item in allTasks) {
                if(SelectedUserIds.Count == 0 || (item.Assignee != null && SelectedUserIds.Contains(item.Assignee.Id)))
                   Tasks.Add(item);
            }
            Tasks.RaiseListChangedEvents = true;
            Tasks.ResetBindings();
        }
        public void InitDocuments() {
            var tdms = this.GetService<IDocumentManagerService>("TabbedView");
            var cardDocument = tdms.CreateDocument(CardViewModel);
            cardDocument.Title = "Board";
            cardDocument.Show();
            var ganttDocument = tdms.CreateDocument(GanttViewModel);
            ganttDocument.Title = "Gantt";
            ganttDocument.Show();
        }
        void OnToggleMessageReceived(ToggleDetailsWindowMessage message) {
                ToggleDetailsService?.ShowDetails(message.Show);
        }
    }
}
