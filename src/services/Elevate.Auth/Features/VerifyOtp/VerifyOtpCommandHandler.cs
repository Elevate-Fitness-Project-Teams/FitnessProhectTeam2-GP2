using AuthService.Infrastructure.Persistence;
using Elevate.Auth.Domain.Entities;
using Elevate.Auth.Domain.Errors;
using Elevate.Auth.Infrastructure.Identity;
using Elevate.Auth.Infrastructure.Persistence;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using SharedKernel;
using System.Threading;
using System.Threading.Tasks;

namespace Elevate.Auth.Features.VerifyOtp;

public sealed class VerifyOtpCommandHandler(
    AuthDbContext db,
    UserManager<AppUser> userManager,
    IPasswordHasher<AppUser> passwordHasher) 
    : IRequestHandler<VerifyOtpCommand, Result<VerifyOtpResponse>>
{
    public async Task<Result<VerifyOtpResponse>> Handle(VerifyOtpCommand cmd, CancellationToken ct)
    {
        var user = await userManager.FindByEmailAsync(cmd.Email);
        if (user is null)
        {
            return Result.Failure<VerifyOtpResponse>(new Error(
                AuthErrorCodes.InvalidCredentials, "User not found.", ErrorType.NotFound));
        }

        var otpRecord = await db.OtpCodes
            .FirstOrDefaultAsync(o => o.UserId == user.Id, ct);

        if (otpRecord is null)
        {
            return Result.Failure<VerifyOtpResponse>(new Error(
                AuthErrorCodes.InvalidOtp, "No OTP generated for this user.", ErrorType.Unauthorized));
        }

        var verificationResult = passwordHasher.VerifyHashedPassword(user, otpRecord.CodeHash, cmd.Otp);
        if (verificationResult == PasswordVerificationResult.Failed)
        {
            return Result.Failure<VerifyOtpResponse>(new Error(
                AuthErrorCodes.InvalidOtp, "Invalid OTP code.", ErrorType.Unauthorized));
        }

        var resetToken = await userManager.GeneratePasswordResetTokenAsync(user);

        db.OtpCodes.Remove(otpRecord);
        await db.SaveChangesAsync(ct);

        return Result.Success(new VerifyOtpResponse(resetToken, true));
    }
}