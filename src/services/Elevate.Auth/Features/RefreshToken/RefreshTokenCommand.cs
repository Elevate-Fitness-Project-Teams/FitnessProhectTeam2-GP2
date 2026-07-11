using MediatR;
using SharedKernel;

namespace Elevate.Auth.Features.RefreshToken;

/// US-1.7: POST /api/v1/auth/refresh-token
public sealed record RefreshTokenCommand(string ExpiredAccessToken, string RefreshToken)
    : IRequest<Result<RefreshTokenResponse>>;
