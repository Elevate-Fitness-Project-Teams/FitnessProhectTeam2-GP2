using MediatR;
using SharedKernel;

namespace Elevate.Auth.Features.Login;

public sealed record LoginCommand(
    string Email,
    string Password,
    string IpAddress) : IRequest<Result<LoginResponse>>;


