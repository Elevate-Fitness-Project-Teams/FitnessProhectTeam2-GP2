using Elevate.Auth.Domain.Entities;
using Elevate.Auth.Domain.Errors;
using Elevate.Auth.Domain.ValueObjects;
using Elevate.Auth.Infrastructure.Identity;
using MediatR;
using Microsoft.AspNetCore.Identity;
using SharedKernel;
using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Elevate.Auth.Features.Register;

public sealed class RegisterCommandHandler(
    UserManager<AppUser> userManager)
    : IRequestHandler<RegisterCommand, Result<RegisterResponse>>
{
    public async Task<Result<RegisterResponse>> Handle(RegisterCommand cmd, CancellationToken ct)
    {
        var existingUser = await userManager.FindByEmailAsync(cmd.Email);
        if (existingUser is not null) 
        {
            return Result.Failure<RegisterResponse>(new Error(
                AuthErrorCodes.EmailExists,
                "Email is already registered.",
                ErrorType.Conflict));
        }

        var fullNameResult = FullName.Create(cmd.FirstName, cmd.LastName);
        if (fullNameResult.IsFailure)
        {
            return Result.Failure<RegisterResponse>(fullNameResult.Error);
        }

        var emailResult = Email.Create(cmd.Email);
        if (emailResult.IsFailure)
        {
            return Result.Failure<RegisterResponse>(emailResult.Error);
        }

        var now = DateTime.UtcNow;

        var user = AppUser.RegisterUser(
            fullNameResult.Value,
            emailResult.Value,
            cmd.PhoneNumber,
            now);

        var identityResult = await userManager.CreateAsync(user, cmd.Password);
        if (!identityResult.Succeeded)
        {
            var firstError = identityResult.Errors.First().Description;
            return Result.Failure<RegisterResponse>(new Error(
                AuthErrorCodes.WeakPassword,
                firstError,
                ErrorType.Validation));
        }

        return Result.Success(new RegisterResponse(
            user.Id,
            user.Email!,
            true));
    }
}