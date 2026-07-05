using MediatR;
using SharedKernel;

namespace Elevate.Auth.Features.Logout;


public sealed record LogoutCommand(string RefreshToken, Guid UserId) : IRequest<Result>;
