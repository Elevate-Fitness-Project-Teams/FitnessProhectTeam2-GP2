using MediatR;
using Microsoft.AspNetCore.Mvc;
using SharedKernel;

namespace Elevate.Workout.Features.Workouts.GetWorkoutDetails
{
    public static class GetWorkoutDetailsEndpoint
    {
        public static void MapGetWorkoutDetailsEndpoint(this IEndpointRouteBuilder app)
        {
            app.MapGet("/api/v1/workouts/{id:int}", async (int id, [FromServices] IMediator mediator) =>
            {
                var query = new GetWorkoutDetailsQuery(id);

                var result = await mediator.Send(query);

                if (result.IsFailure)
                {
                    var errorResponse = ApiResponse<object>.Failure(
                        message: result.Error.Message,
                        statusCode: 404,
                        errors: new List<string> { result.Error.Code });

                    return Results.Json(errorResponse, statusCode: 404);
                }

                var successResponse = ApiResponse<WorkoutDetailsResponse>.Success(
                    data: result.Value,
                    message: "Workout details retrieved successfully.",
                    statusCode: 200
                );

                return Results.Ok(successResponse);
            })
            .WithName("GetWorkoutDetails")
            .WithTags("Workouts")
            .RequireAuthorization(); 
        }
    }
}
