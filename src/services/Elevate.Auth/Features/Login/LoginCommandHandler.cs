using AuthService.Infrastructure.Persistence;
using Elevate.Auth.Domain.Entities;
using Elevate.Auth.Domain.Enums;
using Elevate.Auth.Domain.Errors;
using Elevate.Auth.Domain.Interfaces;
using Elevate.Auth.Infrastructure.Identity;
using Elevate.Auth.Infrastructure.Presistence.Interfaces;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using SharedKernel;

namespace Elevate.Auth.Features.Login;

public sealed class LoginCommandHandler(
    AuthDbContext db,
    UserManager<AppUser> userManager,
    IJwtTokenGenerator jwtGenerator,
    ITokenService tokenService,
    IPublisher publisher)
    : IRequestHandler<LoginCommand, Result<LoginResponse>>
{
    
    private const int RefreshTokenDays = 7;

    public async Task<Result<LoginResponse>> Handle(
        LoginCommand cmd, CancellationToken ct)
    {
        var now = DateTime.UtcNow;

        //  Find user with LoginAttempts & RefreshTokens
        var appUser = await userManager.Users
            .Include(u => u.LoginAttempts)
            .Include(u => u.RefreshTokens) 
            .FirstOrDefaultAsync(u => u.Email == cmd.Email, ct);

        if (appUser is null)
            return Result.Failure<LoginResponse>(new Error(AuthErrorCodes.InvalidCredentials, "Invalid email or password.", ErrorType.Unauthorized));

        //  Reject immediately if currently locked
        if (appUser.IsCurrentlyLockedOut(now))
        {
            db.LoginAttempts.Add(LoginAttempt.Create(appUser.Id, false, cmd.IpAddress, now));
            await db.SaveChangesAsync(ct);
            return Result.Failure<LoginResponse>(new Error(AuthErrorCodes.AccountLocked, "Your account is temporarily locked.", ErrorType.Locked));
        }

        // Verify password
        var passwordCorrect = await userManager.CheckPasswordAsync(appUser, cmd.Password);

        // Let AppUser apply lockout business rule internally
        var attemptResult = appUser.EvaluateLockout(passwordCorrect, now);

        db.LoginAttempts.Add(LoginAttempt.Create(appUser.Id, passwordCorrect, cmd.IpAddress, now));

        if (attemptResult != LoginAttemptResult.Success)
        {
            await userManager.UpdateAsync(appUser);
            await db.SaveChangesAsync(ct);

            foreach (var e in appUser.DomainEvents)
                await publisher.Publish(e, ct);
            appUser.ClearDomainEvents();

            return attemptResult == LoginAttemptResult.LockedOut
                ? Result.Failure<LoginResponse>(new Error(AuthErrorCodes.AccountLocked, "Your account has been locked.", ErrorType.Locked))
                : Result.Failure<LoginResponse>(new Error(AuthErrorCodes.InvalidCredentials, "Invalid email or password.", ErrorType.Unauthorized));
        }

        //  Generate JWT 
        var roles = (await userManager.GetRolesAsync(appUser)).ToList();
         
        var jwt = jwtGenerator.GenerateToken(appUser, roles);

        // Generate & Record Refresh Token via Aggregate Root
        var raw = Convert.ToBase64String(
            System.Security.Cryptography.RandomNumberGenerator.GetBytes(64));
        var hash = tokenService.HashRefreshToken(raw);
        var expiry = now.AddDays(RefreshTokenDays);

        appUser.RecordSuccessfulLogin(hash, expiry, now);

        await userManager.UpdateAsync(appUser);
        await db.SaveChangesAsync(ct);

        return Result.Success(new LoginResponse(
            Token: jwt,
            RefreshToken: raw,
            ProfileCompleted: !appUser.RequiresProfileCompletion,
            IsPremium: false,
            FirstName: appUser.Name.FirstName, 
            LastName: appUser.Name.LastName,
            Email: appUser.Email!));
    }
}
    
