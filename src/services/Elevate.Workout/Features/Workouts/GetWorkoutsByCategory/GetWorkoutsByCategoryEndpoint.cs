using MediatR;
using Microsoft.AspNetCore.Mvc;
using SharedKernel;

namespace Elevate.Workout.Features.Workouts.GetWorkoutsByCategory
{
    public static class GetWorkoutsByCategoryEndpoint
    {
        public static void MapGetWorkoutsByCategoryEndpoint(this IEndpointRouteBuilder app)
        {
            app.MapGet("/api/v1/workouts/category/{categoryName}", async (string categoryName, [FromServices] IMediator mediator) =>
            {
                var query = new GetWorkoutsByCategoryQuery(categoryName);

                var result = await mediator.Send(query);

                if (result.IsFailure)
                {
                    var errorResponse = ApiResponse<List<WorkoutByCategoryResponse>>.Failure(
                        message: result.Error.Message,
                        statusCode: 404,
                        errors: [result.Error.Code] 
                    );

                    return Results.Json(errorResponse, statusCode: 404);
                }

                var successResponse = ApiResponse<List<WorkoutByCategoryResponse>>.Success(
                    data: result.Value,
                    message: "Workouts under the specified category retrieved successfully.",
                    statusCode: 200
                );

                return Results.Ok(successResponse);
            })
            .WithName("GetWorkoutsByCategory")
            .WithTags("Workouts")
            .RequireAuthorization();
        }
    }
}
