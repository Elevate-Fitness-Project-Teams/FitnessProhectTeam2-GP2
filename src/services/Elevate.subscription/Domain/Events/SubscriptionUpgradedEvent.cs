using SharedKernel.Interfaces;

namespace Elevate.subscription.Domain.Events
{
    public sealed record SubscriptionUpgradedEvent(Guid SubscriptionId, Guid UserId,
        Guid NewPlanId, DateTime OccurredAt) :IDomainEvent;
}
