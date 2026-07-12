using Elevate.Progress.Integration.Events;

namespace Elevate.Progress.Integration.Events
{
    public sealed record WeightUpdatedEvent(
    Guid UserId,
    decimal NewWeight,
    DateTime RecordedAt);
}

