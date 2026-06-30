using SharedKernel.Interfaces;

namespace Elevate.Auth.Domain.DomainEvents
{
    public sealed record UserLockedOutEvent(
    Guid UserId,
    DateTime LockedUntil,
    DateTime OccurredAt) : IDomainEvent;
}
