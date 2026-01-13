using System;
using System.Collections.Generic;
using DXTrello.Core.Enums;

namespace DXTrello.Core.Models {
    public class ProjectTask {
        // Unique ID from GitHub
        public long Id { get; set; }

        // For Gantt Hierarchy (mapped from GitHub Task Lists or Labels)
        public long? ParentId { get; set; }

        public string Title { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }

        // Percent complete (0-1). Used by the Gantt view.
        public double Progress { get; set; }

        // For Kanban Columns
        public ProjectTaskStatus Status { get; set; }

        // For Resource Mapping
        public TeamMember? Assignee { get; set; }

        // Dependency tracking (GitHub issue links)
        public IList<long> DependencyIds { get; set; } = new List<long>();

        public string StatusDisplay => Status.ToString();
        public string AssigneeName => Assignee?.DisplayName ?? Assignee?.Login ?? string.Empty;
    }
}
