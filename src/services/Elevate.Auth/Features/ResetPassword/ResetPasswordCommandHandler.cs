using AuthService.Infrastructure.Persistence;
using Elevate.Auth.Infrastructure.Identity;
using Elevate.Auth.Infrastructure.Persistence;
using Elevate.Auth.Infrastructure.Presistence.Interfaces;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using SharedKernel;

namespace Elevate.Auth.Features.ResetPassword;

/// <summary>
/// US-1.6: Validates the resetToken, updates the password hash,
/// revokes all active refresh tokens, marks OtpCode row as used.
/// Tables: Users (Update PasswordHash), OtpCodes (Update IsUsed),
///         RefreshTokens (Update RevokedAt).
/// </summary>
public sealed class ResetPasswordCommandHandler(
    UserManager<AppUser> userManager,
    AuthDbContext db,
    ITokenService tokenService) 
    : IRequestHandler<ResetPasswordCommand, Result<ResetPasswordResponse>>
{
    public async Task<Result<ResetPasswordResponse>> Handle(ResetPasswordCommand cmd, CancellationToken ct)
    {
        var incomingTokenHash = tokenService.HashResetToken(cmd.ResetToken);

        var user = await db.Users
            .Include(u => u.OtpCodes) // Eager load the collection containing token information records
            .FirstOrDefaultAsync(u => u.OtpCodes.Any(t => t.CodeHash == incomingTokenHash), ct);

        if (user is null)
        {
            return Result.Failure<ResetPasswordResponse>(new Error(
                "INVALID_RESET_TOKEN", "The provided reset token is invalid or unrecognized.", ErrorType.Unauthorized));
        }

        var tokenRecord = user.OtpCodes.FirstOrDefault(t => t.CodeHash == incomingTokenHash);

        if (tokenRecord is null || tokenRecord.ExpiresAt < DateTime.UtcNow)
        {
            return Result.Failure<ResetPasswordResponse>(new Error(
                "EXPIRED_RESET_TOKEN", "The reset token has expired. Please initiate the request again.", ErrorType.Unauthorized));
        }

        var tokenGeneratedByIdentity = await userManager.GeneratePasswordResetTokenAsync(user);
        var identityResult = await userManager.ResetPasswordAsync(user, tokenGeneratedByIdentity, cmd.NewPassword);

        if (!identityResult.Succeeded)
        {
            var firstError = identityResult.Errors.FirstOrDefault();
            return Result.Failure<ResetPasswordResponse>(new Error(
                "PASSWORD_RESET_FAILED", firstError?.Description ?? "Failed to change the password.", ErrorType.Failure));
        }

        user.ResetPassword(DateTime.UtcNow);

        db.OtpCodes.Remove(tokenRecord);
        await userManager.UpdateAsync(user);

        return Result.Success(new ResetPasswordResponse(true));
    }

}
