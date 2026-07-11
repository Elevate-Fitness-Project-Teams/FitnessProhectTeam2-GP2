namespace Elevate.Profile.Application.Features.Profiles.DTO
{
    public class ViewSettingsDto
    {
        public UserPreferencesDto Preferences { get; set; }

        public NotificationSettingsDto Notifications { get; set; }

        public PrivacySettingsDto Privacy { get; set; }
    }
}
