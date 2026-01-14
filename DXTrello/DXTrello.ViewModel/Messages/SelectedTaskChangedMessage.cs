using DXTrello.Core.Models;

namespace DXTrello.ViewModel.Messages {
    public class SelectedTaskChangedMessage {
        public SelectedTaskChangedMessage(ProjectTask? selectedTask) {
            SelectedTask = selectedTask;
        }
        public ProjectTask? SelectedTask { get; }
    }
}
