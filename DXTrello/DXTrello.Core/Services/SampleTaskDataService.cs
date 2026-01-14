using DXTrello.Core.Enums;
using DXTrello.Core.Models;

namespace DXTrello.Core.Services {
    public sealed class SampleTaskDataService : ITaskDataService {
        public Task<IList<ProjectTask>> GetProjectTasksAsync(CancellationToken cancellationToken = default) {
            var team = new[] {
                    new TeamMember { Id = 1, Login = "octocat", DisplayName = "Octo Cat" },
                    new TeamMember { Id = 2, Login = "spacewhale", DisplayName = "Space Whale" },
                    new TeamMember { Id = 3, Login = "dev-bot", DisplayName = "Automation Bot" }
                };

            var today = DateTime.Today;
            var tasks = new List<ProjectTask> {
                    new() {
                        Id = 1,
                        Title = "Plan backlog",
                        Description = "Roughly group GitHub issues by milestone",
                        StartDate = today,
                        EndDate = today.AddDays(2),
                        Status = ProjectTaskStatus.ToDo,
                        Progress = 0.2,
                        Assignee = team[0]
                    },
                    new() {
                        Id = 2,
                        Title = "Implement kanban board",
                        Description = "Tile view bound to shared collection",
                        StartDate = today.AddDays(-1),
                        EndDate = today.AddDays(4),
                        Status = ProjectTaskStatus.InProgress,
                        Progress = 0.45,
                        Assignee = team[1]
                    },
                    new() {
                        Id = 3,
                        ParentId = 2,
                        Title = "Create card template",
                        Description = "Configure TileView layout",
                        StartDate = today.AddDays(-1),
                        EndDate = today.AddDays(1),
                        Status = ProjectTaskStatus.Review,
                        Progress = 0.65,
                        Assignee = team[1]
                    },
                    new() {
                        Id = 4,
                        Title = "Draft gantt view",
                        Description = "Map ProjectTask dates to chart",
                        StartDate = today.AddDays(-2),
                        EndDate = today.AddDays(5),
                        Status = ProjectTaskStatus.InProgress,
                        Progress = 0.5,
                        Assignee = team[2],
                        DependencyIds = { 2 }
                    },
                    new() {
                        Id = 5,
                        ParentId = 4,
                        Title = "Bind TreeList",
                        Description = "aregouseahr iguaerhogiuaye hroifguaeh oriagyuhe rufygaekur fgyakeurfgy aoelurfgy kaeurfgy kseurgy hlaeurfgykaeurfgy kaeurfgy ksuerfgyv kaeu rfgyksduyrfgv alekry fglaery fgbakeurfykaesulrgy akeurfyg lryfg aleriufgy skeurfgy akeur fgyakeur fgykaeurfgy skeurfy akeur fgykeaurfgy akeurfgy kaeurfgy skeurvy kaue rfgykaeu fgyakeur fgysekurfgy kaeurfgy skeu rfgykaweur fgyakweurfgy kawuerfgyekasurfyg aeku rfgyakwue rfgykseaurfykauwefgy akewjryfg akuwerfgyakuwer fgyawuey",
                        StartDate = today.AddDays(-2),
                        EndDate = today.AddDays(2),
                        Status = ProjectTaskStatus.Done,
                        Progress = 1.0,
                        Assignee = team[2]
                    }
                };
            return Task.FromResult(tasks as IList<ProjectTask>);
        }
    }
}
