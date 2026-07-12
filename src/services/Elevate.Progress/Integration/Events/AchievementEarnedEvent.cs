namespace Elevate.Progress.Integration.Events
{
    public sealed record AchievementEarnedEvent(
       Guid UserId,
       Guid AchievementId,
       DateTime EarnedAt);
}
