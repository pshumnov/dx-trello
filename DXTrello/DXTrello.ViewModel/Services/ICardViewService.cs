using DXTrello.Core.Models;
using DXTrello.ViewModel.ViewModels;

namespace DXTrello.ViewModel.Services {
    public interface ICardViewService {
        void LoadAvatars(IList<TeamMember> users);
        void MergeBar();
    }
}
