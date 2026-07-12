using MediatR;

namespace Elevate.Progress.Integration.Events
{
    public sealed record AchievementEarnedIntegrationEvent(
     AchievementEarnedEvent EventData
 ) : INotification;
}
