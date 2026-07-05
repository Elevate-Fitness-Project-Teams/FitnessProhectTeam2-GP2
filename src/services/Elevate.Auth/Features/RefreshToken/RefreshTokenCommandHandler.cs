using AuthService.Infrastructure.Persistence;
using Elevate.Auth.Domain.Entities;
using Elevate.Auth.Infrastructure.Identity;
using Elevate.Auth.Infrastructure.Persistence;
using Elevate.Auth.Infrastructure.Presistence.Interfaces;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using SharedKernel;
using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Elevate.Auth.Features.RefreshToken;

public sealed class RefreshTokenCommandHandler(
    UserManager<AppUser> userManager,
    AuthDbContext db,
    ITokenService tokenService) 
    : IRequestHandler<RefreshTokenCommand, Result<RefreshTokenResponse>>
{
    public async Task<Result<RefreshTokenResponse>> Handle(RefreshTokenCommand cmd, CancellationToken ct)
    {
        var incomingHash = tokenService.HashRefreshToken(cmd.RefreshToken);

        var user = await db.Users
            .Include(u => u.RefreshTokens) 
            .FirstOrDefaultAsync(u => u.RefreshTokens.Any(t => t.TokenHash == incomingHash), ct);

        if (user is null)
        {
            return Result.Failure<RefreshTokenResponse>(new Error(
                "INVALID_REFRESH_TOKEN", "Invalid refresh token.", ErrorType.Unauthorized));
        }

        var existingToken = user.RefreshTokens.FirstOrDefault(t => t.TokenHash == incomingHash);

        if (existingToken is null || existingToken.ExpiresAt < DateTime.UtcNow || existingToken.IsRevoked)
        {
            return Result.Failure<RefreshTokenResponse>(new Error(
                "INVALID_REFRESH_TOKEN", "Token is expired or revoked.", ErrorType.Unauthorized));
        }

        existingToken.Revoke(DateTime.UtcNow);

        var (jwt, rawRefreshToken, refreshTokenHash, expiresAt) = tokenService.GenerateTokenPair(user.Id);

        user.RecordSuccessfulLogin(refreshTokenHash, expiresAt, DateTime.UtcNow);

       var result = await userManager.UpdateAsync(user);
        if (!result.Succeeded)
        {
            var firstError = result.Errors.First().Description;
            return Result.Failure<RefreshTokenResponse>(new Error(
                "TOKEN_REFRESH_FAILED", firstError, ErrorType.Failure));
        }
        return Result.Success(new RefreshTokenResponse(jwt, rawRefreshToken));
    }
}