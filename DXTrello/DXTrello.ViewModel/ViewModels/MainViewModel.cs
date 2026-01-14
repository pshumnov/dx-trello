using DXTrello.Core.Models;
using DXTrello.Core.Services;
using DevExpress.Mvvm.DataAnnotations;
using System.ComponentModel;
using DevExpress.Mvvm.POCO;
using DevExpress.Mvvm;

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
            SelectedViewIndex = 0;

            CardViewModel = CardViewModel.Create(Tasks).SetParentViewModel(this);
            GanttViewModel = GanttViewModel.Create(Tasks).SetParentViewModel(this);
        }

        public virtual CardViewModel CardViewModel { get; protected set; }
        public virtual GanttViewModel GanttViewModel { get; protected set; }

        public string Title { get; }
        public virtual BindingList<ProjectTask> Tasks { get; protected set; }
        public virtual ProjectTask? SelectedTask { get; set; }
        public virtual int SelectedViewIndex { get; set; }

        public async Task OnViewLoad() {
            await LoadProjectTasks();
            InitDocuments();
        }
        public async Task LoadProjectTasks() {
            var data = await taskDataService.GetProjectTasksAsync().ConfigureAwait(false);
            Tasks.Clear();
            foreach(var item in data)
                Tasks.Add(item);
            SelectedTask = Tasks.FirstOrDefault();
        }
        public void InitDocuments() {
            var dms = this.GetService<IDocumentManagerService>();
            var cardDocument = dms.CreateDocument(CardViewModel);
            cardDocument.Title = "Board";
            cardDocument.Show();
            var ganttDocument = dms.CreateDocument(GanttViewModel);
            ganttDocument.Title = "Gantt";
            ganttDocument.Show();
        }
    }
}
