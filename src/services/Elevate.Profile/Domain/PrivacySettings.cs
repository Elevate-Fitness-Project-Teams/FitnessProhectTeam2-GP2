using Elevate.Profile.Domain.Enums;
using System.ComponentModel.DataAnnotations;

namespace Elevate.Profile.Domain
{
    public class PrivacySettings
    {
        public int UserId { get; set; }
        
        public ProfileVisibility ProfileVisibility { get; set; }= ProfileVisibility.Private;

        public bool ShowProgressToFriends { get; set; } = false;
        public bool AllowDataSharing { get; set; } = false;
    }
}
