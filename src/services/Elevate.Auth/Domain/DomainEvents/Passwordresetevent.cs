using SharedKernel.Interfaces;

namespace Elevate.Auth.Domain.DomainEvents
{
    public sealed record PasswordResetEvent(
    Guid UserId,
    DateTime OccurredAt) : IDomainEvent;
}
