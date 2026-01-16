using DXTrello.Core.Models;
using DXTrello.Core.Services;
using DevExpress.Mvvm.DataAnnotations;
using System.ComponentModel;
using DevExpress.Mvvm.POCO;
using DevExpress.Mvvm;
using DXTrello.ViewModel.Services;
using DXTrello.ViewModel.Messages;

namespace DXTrello.ViewModel.ViewModels {
    [POCOViewModel()]
    public class MainViewModel {
        private readonly ITaskDataService taskDataService;

        public MainViewModel() : this(new SampleTaskDataService()) {
        }

        public MainViewModel(ITaskDataService taskDataService) {
            this.taskDataService = taskDataService ?? throw new ArgumentNullException(nameof(taskDataService));
            Title = "DXTrello";
            Tasks = [];
            Users = [];
            SelectedViewIndex = 0;
            Messenger.Default.Register<ToggleDetailsWindowMessage>(this, OnToggleMessageReceived);

            CardViewModel = CardViewModel.Create(Tasks, Users).SetParentViewModel(this);
            GanttViewModel = GanttViewModel.Create(Tasks).SetParentViewModel(this);
            DetailsViewModel = DetailsViewModel.Create().SetParentViewModel(this);
        }
        public virtual CardViewModel CardViewModel { get; protected set; }
        public virtual GanttViewModel GanttViewModel { get; protected set; }
        public virtual DetailsViewModel DetailsViewModel { get; protected set; }
        public virtual BindingList<ProjectTask> Tasks { get; protected set; }
        public virtual BindingList<TeamMember> Users { get; protected set; }
        public string Title { get; }
        public virtual int SelectedViewIndex { get; set; }
        public virtual IToggleDetailsService ToggleDetailsService => this.GetService<IToggleDetailsService>();

        public async Task OnViewLoad() {
            await LoadProjectTasks();
            await LoadUsersAsync();
            InitDocuments();
        }
        async Task LoadProjectTasks() {
            var data = await taskDataService.GetProjectTasksAsync().ConfigureAwait(false);
            Tasks.Clear();
            foreach(var item in data)
                Tasks.Add(item);
        }
        async Task LoadUsersAsync() {
            var members = await taskDataService.GetTeamMembersAsync();
            Users.Clear();
            foreach(var user in members)
                Users.Add(user);
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
