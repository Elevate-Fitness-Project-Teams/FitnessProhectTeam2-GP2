namespace Elevate.Profile.Application.Features.Profiles.DTO
{
    public class NotificationSettingsDto
    {
        public bool WorkoutReminders { get; set; } = true;

        public bool MealReminders { get; set; } = true;

        public bool AchievementAlerts { get; set; } = true;

        public bool WeeklyReports { get; set; } = true;

        public bool EmailNotifications { get; set; } = true;

        public bool PushNotifications { get; set; } = true;

    }
}