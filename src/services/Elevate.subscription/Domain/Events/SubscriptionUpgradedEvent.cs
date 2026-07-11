using Elevate.Subscription.Domain.Enums;
using SharedKernel.Interfaces;

namespace Elevate.subscription.Domain.Events
{
    public sealed record SubscriptionUpgradedEvent(
       Guid SubscriptionId,
       Guid UserId,
       SubscriptionTier Tier,
       DateTime ExpiresAt,
       DateTime OccurredAt) : IDomainEvent;
}
