using Elevate.Subscription.Domain.Enums;
using SharedKernel.Interfaces;

namespace Elevate.subscription.Domain.Events
{
    public sealed record SubscriptionCancelledEvent(
    Guid SubscriptionId,
    Guid UserId,
    DateTime? ExpiresAt,
    DateTime OccurredAt) : IDomainEvent;
}
