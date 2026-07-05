using MediatR;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Routing;
using SharedKernel;
using System;
using System.Security.Claims;
using System.Threading;

namespace Elevate.Auth.Features.CompleteProfile;

public static class CompleteProfileEndpoint
{
    public static void MapCompleteProfileEndpoint(this IEndpointRouteBuilder app)
    {
        app.MapPost("api/auth/complete-profile", async (
            [FromServices] IMediator mediator,
            HttpContext context,
            CancellationToken ct) =>
        {
            var userIdClaim = context.User.FindFirst(ClaimTypes.NameIdentifier)?.Value;

            if (string.IsNullOrEmpty(userIdClaim) || !Guid.TryParse(userIdClaim, out var userIdGuid))
            {
                return Results.Json(
                    ApiResponse<CompleteProfileResponse>.Failure("Unauthorized access. Token is missing or invalid.", StatusCodes.Status401Unauthorized),
                    statusCode: StatusCodes.Status401Unauthorized);
            }

            var command = new CompleteProfileCommand(userIdGuid);

            var result = await mediator.Send(command, ct);

            if (result.IsSuccess)
            {
                return Results.Ok(ApiResponse<CompleteProfileResponse>.Success(
                    result.Value,
                    "Profile state has been updated successfully."));
            }

            return Results.BadRequest(ApiResponse<CompleteProfileResponse>.Failure(
                result.Error.Message,
                StatusCodes.Status400BadRequest));
        })
        .WithName("CompleteProfile")
        .WithTags("Authentication")
        .RequireAuthorization();
    }
}