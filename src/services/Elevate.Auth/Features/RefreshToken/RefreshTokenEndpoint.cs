using MediatR;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Routing;
using SharedKernel;

namespace Elevate.Auth.Features.RefreshToken;

public static class RefreshTokenEndpoint
{
    public static void MapRefreshTokenEndpoint(this IEndpointRouteBuilder app)
    {
        app.MapPost("api/auth/refresh-token", async (
            [FromBody] RefreshTokenRequest request,
            [FromServices] IMediator mediator,
            CancellationToken ct) =>
        {
            var command = new RefreshTokenCommand(request.AccessToken, request.RefreshToken);

            var result = await mediator.Send(command, ct);

            // If the operational logic succeeds, return the newly generated pair of tokens
            if (result.IsSuccess)
            {
                return Results.Ok(ApiResponse<RefreshTokenResponse>.Success(result.Value, "Token refreshed successfully."));
            }

            return Results.BadRequest(ApiResponse<RefreshTokenResponse>.Failure(result.Error.Message, StatusCodes.Status400BadRequest));
        })
        .WithName("RefreshToken")
        .WithTags("Authentication"); 
    }
}

public sealed record RefreshTokenRequest(string AccessToken, string RefreshToken);