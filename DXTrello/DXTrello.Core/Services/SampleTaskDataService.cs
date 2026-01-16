using DXTrello.Core.Enums;
using DXTrello.Core.Models;

namespace DXTrello.Core.Services {
    public sealed class SampleTaskDataService : ITaskDataService {
        public static IList<TeamMember> Users = new List<TeamMember> {
                new TeamMember { Id = 1, Login = "octocat", DisplayName = "Octo Cat", AvatarUrl = "https://cdn.jsdelivr.net/gh/alohe/avatars/png/vibrent_1.png" },
                new TeamMember { Id = 2, Login = "spacewhale", DisplayName = "Space Whale", AvatarUrl = "https://raw.githubusercontent.com/PokeAPI/sprites/master/sprites/pokemon/other/official-artwork/321.png" },
                new TeamMember { Id = 3, Login = "dev-bot", DisplayName = "Automation Bot", AvatarUrl = "https://avatars.githubusercontent.com/in/15368?s=64&v=4" }
            };
        public Task<IList<ProjectTask>> GetProjectTasksAsync(CancellationToken cancellationToken = default) {
            var today = DateTime.Today;
            var tasks = new List<ProjectTask> {
                    new() {
                        Id = 1,
                        Title = "Initial Requirements",
                        Description = "Gather requests from stakeholders",
                        StartDate = today.AddDays(-14),
                        EndDate = today.AddDays(-12),
                        Status = ProjectTaskStatus.Done,
                        Assignee = Users[0]
                    },
                    new() {
                        Id = 2,
                        Title = "Architecture Design",
                        Description = "Define solution structure and core interfaces",
                        StartDate = today.AddDays(-11),
                        EndDate = today.AddDays(-9),
                        Status = ProjectTaskStatus.Done,
                        Assignee = Users[1],
                        DependencyIds = { 1 }
                    },
                    new() {
                        Id = 3,
                        Title = "Setup Repository",
                        Description = "Initialize git and create projects",
                        StartDate = today.AddDays(-8),
                        EndDate = today.AddDays(-7),
                        Status = ProjectTaskStatus.Done,
                        Assignee = Users[2],
                        DependencyIds = { 2 }
                    },
                    new() {
                        Id = 4,
                        Title = "Core Data Models",
                        Description = "Create POCOs and Enums",
                        StartDate = today.AddDays(-6),
                        EndDate = today.AddDays(-5),
                        Status = ProjectTaskStatus.Done,
                        Assignee = Users[0],
                        DependencyIds = { 3 }
                    },
                    new() {
                        Id = 5,
                        Title = "Implement Kanban Board",
                        Description = "Tile view bound to shared collection",
                        StartDate = today.AddDays(-4),
                        EndDate = today.AddDays(3),
                        Status = ProjectTaskStatus.InProgress,
                        Assignee = Users[1],
                        DependencyIds = { 4 }
                    },
                    new() {
                        Id = 6,
                        ParentId = 5,
                        Title = "Design Card Template",
                        Description = "Configure TileView HTML template",
                        StartDate = today.AddDays(-3),
                        EndDate = today.AddDays(-1),
                        Status = ProjectTaskStatus.Review,
                        Assignee = Users[1]
                    },
                    new() {
                        Id = 7,
                        Title = "Draft Gantt View",
                        Description = "Map ProjectTask dates to chart",
                        StartDate = today.AddDays(-2),
                        EndDate = today.AddDays(5),
                        Status = ProjectTaskStatus.InProgress,
                        Assignee = Users[2]
                    },
                    new() {
                        Id = 8,
                        ParentId = 7,
                        Title = "Configure Gantt Mappings",
                        Description = "Bind TreeList and Chart mappings",
                        StartDate = today.AddDays(-1),
                        EndDate = today.AddDays(1),
                        Status = ProjectTaskStatus.InProgress,
                        Assignee = Users[2]
                    },
                    new() {
                        Id = 9,
                        Title = "GitHub API Sync Service",
                        Description = "Implement Octokit integration",
                        StartDate = today.AddDays(4),
                        EndDate = today.AddDays(8),
                        Status = ProjectTaskStatus.ToDo,
                        Assignee = Users[0],
                        DependencyIds = { 5, 7 }
                    },
                    new() {
                        Id = 10,
                        Title = "Final Polish & Release",
                        Description = "Cleanup and testing",
                        StartDate = today.AddDays(9),
                        EndDate = today.AddDays(14),
                        Status = ProjectTaskStatus.ToDo,
                        Assignee = Users[1],
                        DependencyIds = { 9 }
                    }
                };
            return Task.FromResult(tasks as IList<ProjectTask>);
        }
        public Task<IList<TeamMember>> GetTeamMembersAsync(CancellationToken cancellationToken = default) {
            return Task.FromResult(Users);
        }
    }
}
