using MediatR;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Routing;
using SharedKernel;

namespace Elevate.Auth.Features.Register;

public static class RegisterEndpoint
{
    public static void MapRegisterEndpoint(this IEndpointRouteBuilder app)
    {
        app.MapPost("api/auth/register", async (
            [FromBody] RegisterRequest request,
            [FromServices] IMediator mediator,
            CancellationToken ct) =>
        {
            var command = new RegisterCommand(
                request.FirstName,
                request.LastName,
                request.Email,
                request.Password,
                request.PhoneNumber);

            var result = await mediator.Send(command, ct);

            if (result.IsSuccess)
            {
                return Results.Ok(ApiResponse<RegisterResponse>.Success(result.Value, "Registration successful."));
            }

            return result.Error.Type switch
            {
                ErrorType.Conflict =>
                    Results.Json(ApiResponse<RegisterResponse>.Conflict(result.Error.Message), statusCode: StatusCodes.Status409Conflict),
                _ =>
                    Results.BadRequest(ApiResponse<RegisterResponse>.Failure(result.Error.Message, StatusCodes.Status400BadRequest))
            };
        })
        .WithName("Register")
        .WithTags("Authentication")
        .Produces<ApiResponse<RegisterResponse>>(StatusCodes.Status200OK)
        .Produces<ApiResponse<RegisterResponse>>(StatusCodes.Status409Conflict)
        .Produces<ApiResponse<RegisterResponse>>(StatusCodes.Status400BadRequest);
    }
}

public sealed record RegisterRequest(
    string Email,
    string Password,
    string FirstName,
    string LastName,
    string PhoneNumber);