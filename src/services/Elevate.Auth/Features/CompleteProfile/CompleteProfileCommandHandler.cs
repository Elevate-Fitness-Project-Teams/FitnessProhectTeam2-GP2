using Elevate.Auth.Domain.Entities;
using Elevate.Auth.Infrastructure.Identity;
using MediatR;
using Microsoft.AspNetCore.Identity;
using SharedKernel;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace Elevate.Auth.Features.CompleteProfile;

public sealed class CompleteProfileCommandHandler(UserManager<AppUser> userManager)
    : IRequestHandler<CompleteProfileCommand, Result<CompleteProfileResponse>>
{
    public async Task<Result<CompleteProfileResponse>> Handle(CompleteProfileCommand cmd, CancellationToken ct)
    {
        var user = await userManager.FindByIdAsync(cmd.UserId.ToString());

        if (user is null)
        {
            return Result.Failure<CompleteProfileResponse>(new Error(
                "USER_NOT_FOUND", "User profile not found.", ErrorType.NotFound));
        }

        if (!user.RequiresProfileCompletion)
        {
            return Result.Success(new CompleteProfileResponse(true));
        }

        user.CompleteProfile(DateTime.UtcNow);

        var identityResult = await userManager.UpdateAsync(user);

        if (!identityResult.Succeeded)
        {
            return Result.Failure<CompleteProfileResponse>(new Error(
                "PROFILE_UPDATE_FAILED", "Failed to update profile data.", ErrorType.Failure));
        }

        return Result.Success(new CompleteProfileResponse(true));
    }
}