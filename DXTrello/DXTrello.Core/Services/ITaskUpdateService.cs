using System.Threading;
using System.Threading.Tasks;
using DXTrello.Core.Models;

namespace DXTrello.Core.Services {
    public interface ITaskUpdateService {
        Task<ProjectTask> UpdateTaskAsync(ProjectTask task, CancellationToken cancellationToken = default);
    }
}
