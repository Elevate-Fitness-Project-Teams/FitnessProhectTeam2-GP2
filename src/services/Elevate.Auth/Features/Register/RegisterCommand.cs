using MediatR;
using SharedKernel;

namespace Elevate.Auth.Features.Register;

public sealed record RegisterCommand(
    string FirstName,
    string LastName,
    string Email,
    string Password,
    string PhoneNumber) : IRequest<Result<RegisterResponse>>;


