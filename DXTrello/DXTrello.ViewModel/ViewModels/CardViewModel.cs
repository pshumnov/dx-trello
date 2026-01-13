using DXTrello.Core.Models;
using DevExpress.Mvvm.DataAnnotations;
using System.ComponentModel;
using DevExpress.Mvvm.POCO;

namespace DXTrello.ViewModel.ViewModels {
    [POCOViewModel()]
    public class CardViewModel {
        protected CardViewModel(BindingList<ProjectTask> tasks) {
            Tasks = tasks;
        }
        public static CardViewModel Create(BindingList<ProjectTask> tasks) {
            return ViewModelSource.Create(() => new CardViewModel(tasks));
        }
        public virtual BindingList<ProjectTask> Tasks { get; protected set; }
    }
}
