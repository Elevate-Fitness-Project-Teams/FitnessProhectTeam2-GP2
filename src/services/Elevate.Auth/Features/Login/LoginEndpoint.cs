using Elevate.Auth.Domain.Errors;
using FluentValidation;
using MediatR;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Routing;
using SharedKernel;

namespace Elevate.Auth.Features.Login;

public static class LoginEndpoint
{
    public static void MapLoginEndpoint(this IEndpointRouteBuilder app)
    {
        app.MapPost("api/auth/login", async (
            [FromBody] LoginRequest request,
            [FromServices] IMediator mediator,
            [FromServices] IValidator<LoginCommand> validator,
            HttpContext context,
            CancellationToken ct) =>
        {
            var ipAddress = context.Connection.RemoteIpAddress?.ToString() ?? "Unknown";
            var command = new LoginCommand(request.Email, request.Password, ipAddress);

            // 1. Validation check
            var validationResult = await validator.ValidateAsync(command, ct);
            if (!validationResult.IsValid)
            {
                return Results.ValidationProblem(validationResult.ToDictionary());
            }

            var result = await mediator.Send(command, ct);

            if (result.IsSuccess)
            {
                return Results.Ok(ApiResponse<LoginResponse>.Success(result.Value, "Login successful"));
            }

            return result.Error.Type switch
            {
                ErrorType.Locked =>
                    Results.Json(ApiResponse<LoginResponse>.Locked(result.Error.Message), statusCode: StatusCodes.Status423Locked),

                ErrorType.Unauthorized =>
                    Results.Json(ApiResponse<LoginResponse>.Unauthorized(result.Error.Message), statusCode: StatusCodes.Status401Unauthorized),

                _ =>
                    Results.BadRequest(ApiResponse<LoginResponse>.Failure(result.Error.Message, StatusCodes.Status400BadRequest))
            };
        })
        .WithName("Login")
        .WithTags("Authentication")
        .Produces<ApiResponse<LoginResponse>>(StatusCodes.Status200OK)
        .Produces(StatusCodes.Status400BadRequest)
        .Produces(StatusCodes.Status401Unauthorized)
        .Produces(StatusCodes.Status423Locked);
    }
}

public sealed record LoginRequest(string Email, string Password);