using MediatR;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Routing;
using SharedKernel;

namespace Elevate.Auth.Features.VerifyOtp;

public static class VerifyOtpEndpoint
{
    public static void MapVerifyOtpEndpoint(this IEndpointRouteBuilder app)
    {
        app.MapPost("api/auth/verify-otp", async (
            [FromBody] VerifyOtpRequest request,
            [FromServices] IMediator mediator,
            CancellationToken ct) =>
        {
            var command = new VerifyOtpCommand(request.Email, request.OtpCode);
            var result = await mediator.Send(command, ct);

            if (result.IsSuccess)
            {
                return Results.Ok(ApiResponse<VerifyOtpResponse>.Success(result.Value, "OTP verified successfully."));
            }

            return result.Error.Type switch
            {
                ErrorType.Unauthorized =>
                    Results.Json(ApiResponse<VerifyOtpResponse>.Unauthorized(result.Error.Message), statusCode: StatusCodes.Status401Unauthorized),
                _ =>
                    Results.BadRequest(ApiResponse<VerifyOtpResponse>.Failure(result.Error.Message, StatusCodes.Status400BadRequest))
            };
        })
        .WithName("VerifyOtp")
        .WithTags("Authentication");
    }
}

public sealed record VerifyOtpRequest(string Email, string OtpCode);