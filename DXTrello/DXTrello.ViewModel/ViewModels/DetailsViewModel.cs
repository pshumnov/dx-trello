using DevExpress.Mvvm;
using DevExpress.Mvvm.DataAnnotations;
using DevExpress.Mvvm.POCO;
using DXTrello.Core.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Input;

namespace DXTrello.ViewModel.ViewModels {
    [POCOViewModel()]
    public class DetailsViewModel {
        public DetailsViewModel() { }
        protected DetailsViewModel(ProjectTask task) {
            Task = task;
        }
        public static DetailsViewModel Create(ProjectTask task) {
            return ViewModelSource.Create(() => new DetailsViewModel(task));
        }
        public virtual ProjectTask Task { get; protected set; }
        public virtual IDialogService DialogService => this.GetService<IDialogService>();
    }
}
