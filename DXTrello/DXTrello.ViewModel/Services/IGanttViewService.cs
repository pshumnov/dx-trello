using DXTrello.Core.Models;
using DXTrello.ViewModel.ViewModels;

namespace DXTrello.ViewModel.Services {
    public interface IGanttViewService {
        public void ScrollToDate(DateTime date);
        public void ApplyTimescale(TimescaleEnum timescaleUnit, DateTime startDate);
        public ProjectTask? GetFocusedNodeTask();
    }
}
