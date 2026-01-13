using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using DXTrello.Core.Models;

namespace DXTrello.Core.Services {
    public interface ITaskDataService {
        Task<IList<ProjectTask>> GetProjectTasksAsync(CancellationToken cancellationToken = default);
    }
}
