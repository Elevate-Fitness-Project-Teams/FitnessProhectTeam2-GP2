using MediatR;
using Microsoft.AspNetCore.Mvc;
using SharedKernel;

namespace Elevate.Workout.Features.Workouts.GetWorkoutsByPlan
{
    public static class GetWorkoutsByPlanEndpoint
    {
        public static void MapGetWorkoutsByPlanEndpoint(this IEndpointRouteBuilder app)
        {
            app.MapGet("/api/v1/workouts/by-plan/{planId:int}", async (Guid planId, [FromServices] IMediator mediator) =>
            {
                var query = new GetWorkoutsByPlanQuery(planId);

                var result = await mediator.Send(query);

                if (result.IsFailure)
                {
                    var errorResponse = ApiResponse<List<WorkoutByPlanResponse>>.Failure(
                        message: result.Error.Message,
                        statusCode: 404,
                        errors: [result.Error.Code] 
                    );

                    return Results.Json(errorResponse, statusCode: 404);
                }

                var successResponse = ApiResponse<List<WorkoutByPlanResponse>>.Success(
                    data: result.Value,
                    message: "Workouts for the specified plan retrieved successfully.",
                    statusCode: 200
                );

                return Results.Ok(successResponse);
            })
            .WithName("GetWorkoutsByPlan")
            .WithTags("Workouts")
            .RequireAuthorization(); 
        }
    }
}
