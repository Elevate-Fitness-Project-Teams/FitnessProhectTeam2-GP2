using MediatR;
using Microsoft.AspNetCore.Mvc;
using SharedKernel;

namespace Elevate.Workout.Features.Workouts.GetWorkoutPlanDetail
{
    public static class GetWorkoutPlanDetailEndpoint
    {
        public static void MapGetWorkoutPlanDetailEndpoint(this IEndpointRouteBuilder app)
        {
            app.MapGet("/api/v1/workout-plans/{planId:int}", async (Guid planId, [FromServices] ISender sender) =>
            {
                var result = await sender.Send(new GetWorkoutPlanDetailQuery(planId));
                return Results.Ok(ApiResponse<WorkoutPlanDetailResponse>.Success(result.Value));
            })
            .RequireAuthorization()
            .WithName("GetWorkoutPlanDetail")
            .WithTags("WorkoutPlans");
        }
    }
}
