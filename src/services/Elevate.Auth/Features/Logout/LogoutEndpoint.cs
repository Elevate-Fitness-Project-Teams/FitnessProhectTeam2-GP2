using MediatR;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Routing;
using SharedKernel;
using System;
using System.Security.Claims;

namespace Elevate.Auth.Features.Logout;

public static class LogoutEndpoint
{
    public static void MapLogoutEndpoint(this IEndpointRouteBuilder app)
    {
        app.MapPost("api/auth/logout", async (
            [FromBody] LogoutRequest request,
            [FromServices] IMediator mediator,
            HttpContext context,
            CancellationToken ct) =>
        {
            var userIdClaim = context.User.FindFirst(ClaimTypes.NameIdentifier)?.Value;

            if (string.IsNullOrEmpty(userIdClaim) || !Guid.TryParse(userIdClaim, out var userIdGuid))
            {
                return Results.Json(ApiResponse<LogoutResponse>.Unauthorized("Unauthorized access."), statusCode: StatusCodes.Status401Unauthorized);
            }

            var command = new LogoutCommand(request.RefreshToken, userIdGuid);
            var result = await mediator.Send(command, ct);

            if (result.IsSuccess)
            {
                var responseData = new LogoutResponse(true);
                return Results.Ok(ApiResponse<LogoutResponse>.Success(responseData, "Logged out successfully."));
            }

            return Results.BadRequest(ApiResponse<LogoutResponse>.Failure(result.Error.Message, StatusCodes.Status400BadRequest));
        })
        .WithName("Logout")
        .WithTags("Authentication")
        .RequireAuthorization();
    }
}

public sealed record LogoutRequest(string RefreshToken);