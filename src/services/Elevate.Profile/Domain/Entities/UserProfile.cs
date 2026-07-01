namespace Elevate.Profile.Domain.Entities
{
    public class UserProfile
    {
        public int UserId { get; set; }
        public string FirstName { get; set; } = null!;
        public string LastName { get; set; } = null!;
        public string PhoneNumber { get; set; } = null!;
        public string Email { get; set; } = null!;
        public bool IsPremiumCached { get; set; } = false;
        public string? ProfilePictureUrl { get; set; }
        public DateTime MemberSince { get; set; } = DateTime.UtcNow;
        public UserPreferences UserPreferences { get; set; } = null!;
        public PrivacySettings PrivacySettings { get; set; } = null!;
        public NotificationSettings NotificationSettings { get; set; } = null!;

    }
}
