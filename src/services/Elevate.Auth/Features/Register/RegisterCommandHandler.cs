using AuthService.Infrastructure.Persistence;
using Elevate.Auth.Domain.DomainEvents;
using Elevate.Auth.Domain.Entities;
using Elevate.Auth.Domain.Errors;
using Elevate.Auth.Domain.ValueObjects;
using Elevate.Auth.Infrastructure.Identity;
using MassTransit;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using SharedKernel;
using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Elevate.Auth.Features.Register;

public sealed class RegisterCommandHandler(
    UserManager<AppUser> userManager ,IPublishEndpoint publishEndpoint ,AuthDbContext authDb)
    : IRequestHandler<RegisterCommand, Result<RegisterResponse>>
{
    private readonly IPublishEndpoint _publishEndpoint = publishEndpoint;
    private readonly AuthDbContext _authDb = authDb;

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
        //messaging with Profile servie 
        using var transaction = await _authDb.Database.BeginTransactionAsync(ct);
        try
        {
            await publishEndpoint.Publish(new UserRegisteredEvent(
            UserId: user.Id,
            Email: emailResult.Value,
            FirstName: cmd.FirstName,
            LastName: cmd.LastName,
            PhoneNumber: cmd.PhoneNumber,
            OccurredAt: now
        ), ct);
        
        
        var identityResult = await userManager.CreateAsync(user, cmd.Password);
        if (!identityResult.Succeeded)
        {
            var firstError = identityResult.Errors.First().Description;
            return Result.Failure<RegisterResponse>(new Error(
                AuthErrorCodes.WeakPassword,
                firstError,
                ErrorType.Validation));
        }

        await transaction.CommitAsync(ct);

        return Result.Success(new RegisterResponse(
            user.Id,
            user.Email!,
            true));
    }
        catch (Exception)
        {
            await transaction.RollbackAsync(ct);
            throw;
        }
    }
    
}