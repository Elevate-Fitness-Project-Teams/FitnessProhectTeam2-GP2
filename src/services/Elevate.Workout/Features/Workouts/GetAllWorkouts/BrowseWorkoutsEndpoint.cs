using FluentValidation;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Elevate.Workout.Features.Workouts.GetAllWorkouts
{
    public static class BrowseWorkoutsEndpoint
    {
        public static void MapWorkoutEndpoints(this IEndpointRouteBuilder app)
        {
              app.MapGet("/api/v1/workouts", async (
                [AsParameters] GetAllWorkoutsQuery query,
                [FromServices] IMediator mediator,
                [FromServices] IValidator<GetAllWorkoutsQuery> validator) =>
            {
                var validationResult = await validator.ValidateAsync(query);
                if (!validationResult.IsValid)
                {
                    var error = validationResult.Errors.FirstOrDefault();
                    return Results.BadRequest(new { error = error?.ErrorCode, message = error?.ErrorMessage });
                }

                var result = await mediator.Send(query);
                return Results.Ok(result);
            });
        }
    }
}
