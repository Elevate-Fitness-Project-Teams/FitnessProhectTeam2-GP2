using SharedKernel.Interfaces;

namespace Elevate.Auth.Domain.DomainEvents
{
    public sealed record UserProfileCompletedEvent(
    Guid UserId,
    DateTime OccurredAt) : IDomainEvent;
}
