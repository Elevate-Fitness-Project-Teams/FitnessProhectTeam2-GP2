using Elevate.Auth.Domain.ValueObjects;
using SharedKernel.Interfaces;

namespace Elevate.Auth.Domain.DomainEvents;


public sealed record UserRegisteredEvent(
    Guid UserId,
    Email Email,
    DateTime OccurredAt) : IDomainEvent;