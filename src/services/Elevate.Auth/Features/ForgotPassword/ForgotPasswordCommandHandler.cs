using AuthService.Infrastructure.Persistence;
using Elevate.Auth.Domain.Entities;
using Elevate.Auth.Infrastructure.Identity;
using Elevate.Auth.Infrastructure.Persistence;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using SharedKernel;
using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Elevate.Auth.Features.ForgotPassword;

public sealed class ForgotPasswordCommandHandler(
    UserManager<AppUser> userManager,
    AuthDbContext db,
    IPasswordHasher<AppUser> passwordHasher)
    : IRequestHandler<ForgotPasswordCommand, Result<ForgotPasswordResponse>>
{
    private const int OtpExpirySeconds = 600; 
    private const int ResendLockoutSeconds = 30; 
    private static readonly DateTime CraetedAt = DateTime.UtcNow;

    public async Task<Result<ForgotPasswordResponse>> Handle(ForgotPasswordCommand cmd, CancellationToken ct)
    {
        var user = await userManager.FindByEmailAsync(cmd.Email);
        if (user is null)
        {
            return Result.Success(new ForgotPasswordResponse(OtpExpirySeconds, ResendLockoutSeconds));
        }

        var oldOtps = await db.OtpCodes.Where(o => o.UserId == user.Id).ToListAsync(ct);
        if (oldOtps.Any())
        {
            db.OtpCodes.RemoveRange(oldOtps);
        }

        var random = new Random();
        var plainOtp = random.Next(100000, 999999).ToString();

        var hashedOtp = passwordHasher.HashPassword(user, plainOtp);

        var expiryTime = DateTime.UtcNow.AddSeconds(OtpExpirySeconds);

        var otpRecord =  OtpCode.Create(user.Id, hashedOtp, expiryTime ,CraetedAt);

        db.OtpCodes.Add(otpRecord);
        await db.SaveChangesAsync(ct);

        return Result.Success(new ForgotPasswordResponse(OtpExpirySeconds, ResendLockoutSeconds));
    }
}