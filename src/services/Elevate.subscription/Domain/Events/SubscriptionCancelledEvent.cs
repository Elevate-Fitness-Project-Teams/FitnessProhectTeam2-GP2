using SharedKernel.Interfaces;

namespace Elevate.subscription.Domain.Events
{
    public sealed record SubscriptionCancelledEvent(Guid SubscriptionId, Guid UserId, 
        DateTime OccurredAt) : IDomainEvent;
}
