namespace DXTrello.Core.Models {
    public class TeamMember {
        public long Id { get; set; }
        public string Login { get; set; } = string.Empty;
        public string DisplayName { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public string AvatarUrl { get; set; } = string.Empty;

        public override string ToString() => string.IsNullOrWhiteSpace(DisplayName) ? Login : DisplayName;
    }
}
