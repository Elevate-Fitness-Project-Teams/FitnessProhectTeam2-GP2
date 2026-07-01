using Elevate.Profile.Domain.Enums;
using System.ComponentModel.DataAnnotations;

namespace Elevate.Profile.Domain.Entities
{
    public class PrivacySettings
    {
        public int UserId { get; set; }
        
        public ProfileVisibility ProfileVisibility { get; set; }= ProfileVisibility.Private;

        public bool ShowProgressToFriends { get; set; } = false;
        public bool AllowDataSharing { get; set; } = false;

        public UserProfile UserProfile { get; set; } = null!;
    }
}
