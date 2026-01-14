using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using DXTrello.Core.Enums;

namespace DXTrello.Core.Models {
    public class ProjectTask : INotifyPropertyChanged {
        public event PropertyChangedEventHandler? PropertyChanged;
        void OnPropertyChanged([CallerMemberName] string propertyName = null) => PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));

        bool SetField<T>(ref T field, T value, [CallerMemberName] string propertyName = null) {
            if (EqualityComparer<T>.Default.Equals(field, value)) return false;
            field = value;
            OnPropertyChanged(propertyName);
            return true;
        }

        private long id;
        public long Id { get => id; set => SetField(ref id, value); }

        private long? parentId;
        public long? ParentId { get => parentId; set => SetField(ref parentId, value); }

        private string title = string.Empty;
        public string Title { get => title; set => SetField(ref title, value); }

        private string description = string.Empty;
        public string Description { get => description; set => SetField(ref description, value); }

        private DateTime startDate;
        public DateTime StartDate { get => startDate; set => SetField(ref startDate, value); }

        private DateTime endDate;
        public DateTime EndDate { get => endDate; set => SetField(ref endDate, value); }

        private double progress;
        public double Progress { 
            get => progress; 
            set {
                if (SetField(ref progress, value))
                    OnPropertyChanged(nameof(ProgressPercent));
            }
        }

        public int ProgressPercent {
            get => (int)(Progress * 100);
            set => Progress = value / 100.0;
        }

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
                if (SetField(ref assignee, value))
                    OnPropertyChanged(nameof(AssigneeName));
            }
        }

        // Dependency tracking (GitHub issue links)
        public IList<long> DependencyIds { get; set; } = new List<long>();

        public string StatusDisplay => Status.ToString();
        public string AssigneeName => Assignee?.DisplayName ?? Assignee?.Login ?? string.Empty;
    }
}
