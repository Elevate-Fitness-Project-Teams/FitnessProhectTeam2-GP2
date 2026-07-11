using Elevate.Profile.Domain.ValueObjects;
using System.ComponentModel.DataAnnotations;

namespace Elevate.Profile.Domain.Entities
{
    public class UserProfile
    {
        public int UserId { get; set; }
        public FullName Name { get; private set; }= null!;

        public Email Email { get; set; }=null!;

        [Phone]
        public string PhoneNumber { get; set; } = null!;
        public string? ProfilePictureUrl { get; private set; }

        public bool IsPremiumCached { get; private set; }

        public DateTime MemberSince { get; private set; }

        public UserPreferences Preferences { get; private set; }

        public NotificationSettings NotificationSettings { get; private set; } 

        public PrivacySettings PrivacySettings { get; private set; }

        private UserProfile()
        {
            // Required by EF Core
        }

        private UserProfile(
            int userId, 
            FullName name, 
            Email email, 
            string phoneNumber, 
            string? profilePictureUrl
        )
        {
            UserId = userId;
            Name = name;
            Email = email;
            PhoneNumber = phoneNumber;
            ProfilePictureUrl = profilePictureUrl;
            MemberSince = DateTime.UtcNow;  
        }

        public static UserProfile Create(
        int userId,
        FullName name,
        Email email,
        string phoneNumber,
        string? profilePictureUrl)
         {
                return new UserProfile(
                    userId,
                    name,
                    email,
                    phoneNumber,
                    profilePictureUrl);
            }

        public void UpdateProfile(
            FullName name, 
            Email email, 
            string phoneNumber
         
        )
        {
            Name = name;
            Email = email;
            PhoneNumber = phoneNumber;
            //ProfilePictureUrl = profilePictureUrl;
        }

        public void UpdatePreferences(UserPreferences preferences)
        {
            Preferences = preferences;
        }

        public void UpdateNotificationSettings(NotificationSettings notificationSettings)
        {
            NotificationSettings = notificationSettings;
        }

        public void UpdatePrivacySettings(PrivacySettings privacySettings)
        {
            PrivacySettings = privacySettings;
        }



    }
}
