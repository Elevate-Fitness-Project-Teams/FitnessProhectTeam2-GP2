using MediatR;
using Microsoft.AspNetCore.Mvc;
using SharedKernel;
using SharedKernel.Interfaces;

namespace Elevate.subscription.Features.UpgradeSubscription
{
    public sealed class UpgradeSubscriptionEndpoint : IEndpoint
    {
        public sealed record UpgradeSubscriptionRequest
            (Guid UserId, string PlanTier, int DurationMonths);

        public void MapEndpoint(IEndpointRouteBuilder app)
        {
            app.MapPost("/api/v1/subscription/upgrade", async (
                [FromBody] UpgradeSubscriptionRequest request,
                [FromServices] IMediator mediator) =>
            {
                var command = new UpgradeSubscriptionCommand(request.UserId, request.PlanTier, request.DurationMonths);
                var result = await mediator.Send(command);

                if (result.IsSuccess)
                    return Results.Ok(ApiResponse<UpgradeSubscriptionResponse>.Success(result.Value));

                // Temporary inline mapping until confirmed against the real ResultExtensions helper.
                var statusCode = result.Error.Type switch
                {
                    ErrorType.Validation => StatusCodes.Status400BadRequest,
                    ErrorType.Unauthorized => StatusCodes.Status401Unauthorized,
                    ErrorType.NotFound => StatusCodes.Status404NotFound,
                    ErrorType.Conflict => StatusCodes.Status409Conflict,
                    ErrorType.Locked => StatusCodes.Status423Locked,
                    ErrorType.RateLimit => StatusCodes.Status429TooManyRequests,
                    _ => StatusCodes.Status500InternalServerError
                };

                return Results.Json(
                    ApiResponse<UpgradeSubscriptionResponse>.Failure(result.Error.Message, statusCode, [result.Error.Code]),
                    statusCode: statusCode);
            })
            .RequireAuthorization()
            .WithName("UpgradeSubscription")
            .WithTags("Subscription");
        }
    }
}
