using MediatR;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Routing;
using SharedKernel;

namespace Elevate.Auth.Features.ForgotPassword;

public static class ForgotPasswordEndpoint
{
    public static void MapForgotPasswordEndpoint(this IEndpointRouteBuilder app)
    {
        app.MapPost("api/auth/forgot-password", async (
            [FromBody] ForgotPasswordRequest request,
            [FromServices] IMediator mediator,
            CancellationToken ct) =>
        {
            var command = new ForgotPasswordCommand(request.Email);
            var result = await mediator.Send(command, ct);

            if (result.IsSuccess)
            {
                return Results.Ok(ApiResponse<ForgotPasswordResponse>.Success(
                    result.Value,
                    "If the email exists, an OTP code has been generated."));
            }

            return Results.BadRequest(ApiResponse<ForgotPasswordResponse>.Failure(result.Error.Message, StatusCodes.Status400BadRequest));
        })
        .WithName("ForgotPassword")
        .WithTags("Authentication");
    }
}

public sealed record ForgotPasswordRequest(string Email);