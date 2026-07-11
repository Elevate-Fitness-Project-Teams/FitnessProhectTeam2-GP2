using Elevate.subscription.Features.GetSubscriptionStatus;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using SharedKernel;
using SharedKernel.Interfaces;
namespace Elevate.Subscription.Features.GetSubscriptionStatus
{
    public sealed class GetSubscriptionStatusEndpoint : IEndpoint
    {
        public void MapEndpoint(IEndpointRouteBuilder app)
        {
            app.MapGet("/api/v1/subscription/status/{userId:guid}", async (
                Guid userId,
                [FromServices] IMediator mediator) =>
            {
                var result = await mediator.Send(new GetSubscriptionStatusQuery(userId));
                return Results.Ok(ApiResponse<SubscriptionStatusResponse>.Success(result.Value));
            })
            .RequireAuthorization()
            .WithName("GetSubscriptionStatus")
            .WithTags("Subscription");
        }
    }
}