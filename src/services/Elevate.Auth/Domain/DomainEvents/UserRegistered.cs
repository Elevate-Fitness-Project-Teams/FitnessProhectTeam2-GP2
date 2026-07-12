using Elevate.Auth.Domain.ValueObjects;
using SharedKernel.Interfaces;

namespace Elevate.Auth.Domain.DomainEvents;


public sealed record UserRegisteredEvent(
    Guid UserId,
    Email Email,
    String FirstName,
    string LastName,
    string PhoneNumber,
    DateTime OccurredAt) : IDomainEvent;