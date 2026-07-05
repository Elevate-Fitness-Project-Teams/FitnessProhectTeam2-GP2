using MediatR;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Routing;
using SharedKernel;
using System.Threading;

namespace Elevate.Auth.Features.ResetPassword;

public static class ResetPasswordEndpoint
{
    public static void MapResetPasswordEndpoint(this IEndpointRouteBuilder app)
    {
        app.MapPost("api/auth/reset-password", async (
            [FromBody] ResetPasswordRequest request,
            [FromServices] IMediator mediator,
            CancellationToken ct) =>
        {
            if (request.NewPassword != request.ConfirmPassword)
            {
                return Results.BadRequest(ApiResponse<ResetPasswordResponse>.Failure(
                    "Passwords do not match.",
                    StatusCodes.Status400BadRequest));
            }

            var command = new ResetPasswordCommand(request.Token, request.NewPassword);

            var result = await mediator.Send(command, ct);

            if (result.IsSuccess)
            {
                return Results.Ok(ApiResponse<ResetPasswordResponse>.Success(
                    result.Value,
                    "Password has been reset successfully."));
            }

            return Results.BadRequest(ApiResponse<ResetPasswordResponse>.Failure(
                result.Error.Message,
                StatusCodes.Status400BadRequest));
        })
        .WithName("ResetPassword")
        .WithTags("Authentication");
    }
}

public sealed record ResetPasswordRequest(string Token, string NewPassword, string ConfirmPassword);