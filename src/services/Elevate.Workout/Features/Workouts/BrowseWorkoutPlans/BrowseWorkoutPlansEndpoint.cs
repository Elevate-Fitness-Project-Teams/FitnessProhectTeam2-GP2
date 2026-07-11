using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Elevate.Workout.Features.Workouts.BrowseWorkoutPlans
{
    public static class BrowseWorkoutPlansEndpoint
    {
        public static void MapBrowseWorkoutPlansEndpoint(this IEndpointRouteBuilder app)
        {
            app.MapGet("/api/v1/workout-plans", async (int? page, int? pageSize, [FromServices] IMediator mediator) =>
            {
                var query = new BrowseWorkoutPlansQuery(page ?? 1, pageSize ?? 10);
                var result = await mediator.Send(query);

                return result.IsSuccess
                    ? Results.Ok(result.Value)
                    : Results.BadRequest(result.Error);
            })
            .WithName("BrowseWorkoutPlans")
            .WithTags("WorkoutPlans")
            .RequireAuthorization();
        }
    }
}
