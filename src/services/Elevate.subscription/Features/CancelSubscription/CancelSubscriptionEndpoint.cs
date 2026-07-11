using MediatR;
using SharedKernel;
using SharedKernel.Interfaces;

namespace Elevate.subscription.Features.CancelSubscription
{
    public sealed class CancelSubscriptionEndpoint : IEndpoint
    {
        public void MapEndpoint(IEndpointRouteBuilder app)
        {
            app.MapPost("/api/v1/subscription/cancel", async (
                ICurrentUserService currentUser,
                IMediator mediator) =>
            {
                var result = await mediator.Send(new CancelSubscriptionCommand(currentUser.UserId));

                if (result.IsSuccess)
                    return Results.Ok(ApiResponse<CancelSubscriptionResponse>.Success(result.Value));

                var statusCode = result.Error.Type switch
                {
                    ErrorType.NotFound => StatusCodes.Status404NotFound,
                    _ => StatusCodes.Status500InternalServerError
                };

                return Results.Json(
                    ApiResponse<CancelSubscriptionResponse>.Failure(result.Error.Message, statusCode, [result.Error.Code]),
                    statusCode: statusCode);
            })
            .RequireAuthorization()
            .WithName("CancelSubscription")
            .WithTags("Subscription");
        }
    }
}
