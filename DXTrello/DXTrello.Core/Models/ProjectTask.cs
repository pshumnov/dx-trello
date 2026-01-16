using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using DXTrello.Core.Enums;

namespace DXTrello.Core.Models {
    public class ProjectTask : INotifyPropertyChanged {
        public event PropertyChangedEventHandler? PropertyChanged;
        void OnPropertyChanged([CallerMemberName] string? propertyName = null) => PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));

        bool SetField<T>(ref T field, T value, [CallerMemberName] string? propertyName = null) {
            if (EqualityComparer<T>.Default.Equals(field, value)) return false;
            field = value;
            OnPropertyChanged(propertyName);
            return true;
        }

        string GetWordTrimmedString(string source, int maxLength) {
            if (source.Length <= maxLength)
                return source;
            int finalIndex = source.LastIndexOf(' ', maxLength);
            if(finalIndex <= 0)
                finalIndex = maxLength;
            return source.Substring(0, finalIndex) + "...";
        }

        private long id;
        public long Id { get => id; set => SetField(ref id, value); }

        private long? parentId;
        public long? ParentId { get => parentId; set => SetField(ref parentId, value); }

        private string title = string.Empty;
        public string Title { get => title; set => SetField(ref title, value); }

        private string description = string.Empty;
        public string Description { get => description; set => SetField(ref description, value); }
        public string TrimmedDescription { get => GetWordTrimmedString(Description, 100); }

        private DateTime startDate;
        public DateTime StartDate { get => startDate; set => SetField(ref startDate, value); }

        private DateTime endDate;
        public DateTime EndDate { get => endDate; set => SetField(ref endDate, value); }

        private ProjectTaskStatus status;
        public ProjectTaskStatus Status { 
            get => status; 
            set {
                if (SetField(ref status, value))
                    OnPropertyChanged(nameof(StatusDisplay));
            }
        }

        private TeamMember? assignee;
        public TeamMember? Assignee { 
            get => assignee; 
            set {
                if (SetField(ref assignee, value)) {
                    OnPropertyChanged(nameof(AssigneeName));
                    OnPropertyChanged(nameof(AssigneeAvatar));
                }
            }
        }

        // Dependency tracking (GitHub issue links)
        public IList<long> DependencyIds { get; set; } = new List<long>();

        public string StatusDisplay => Status.ToString();
        public string AssigneeName => Assignee?.DisplayName ?? Assignee?.Login ?? string.Empty;
        public string AssigneeAvatar => Assignee?.AvatarUrl ?? string.Empty;
    }
}
