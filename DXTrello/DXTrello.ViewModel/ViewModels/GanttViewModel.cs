using DXTrello.Core.Models;
using DevExpress.Mvvm.DataAnnotations;
using System.ComponentModel;
using DevExpress.Mvvm.POCO;

namespace DXTrello.ViewModel.ViewModels {
    [POCOViewModel()]
    public class GanttViewModel {
        protected GanttViewModel(BindingList<ProjectTask> tasks) {
            Tasks = tasks;
        }
        public static GanttViewModel Create(BindingList<ProjectTask> tasks) {
            return ViewModelSource.Create(() => new GanttViewModel(tasks));
        }
        public virtual BindingList<ProjectTask> Tasks { get; protected set; }
    }
}
