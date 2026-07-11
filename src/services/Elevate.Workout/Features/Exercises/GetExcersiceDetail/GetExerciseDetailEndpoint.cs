using MediatR;
using Microsoft.AspNetCore.Mvc;
using SharedKernel;

namespace Elevate.Workout.Features.Exercises.GetExcersiceDetail
{
    public static class GetExerciseDetailEndpoint
    {
        public static void MapGetExerciseDetailEndpoint(this IEndpointRouteBuilder app)
        {
            app.MapGet("/api/v1/exercises/{id:int}", async (int id, [FromServices] IMediator mediator) =>
            {
                var query = new GetExerciseDetailQuery(id);
                var result = await mediator.Send(query);

                if (result.IsFailure)
                {
                    var errorResponse = ApiResponse<object>.Failure(result.Error, 404);
                    return Results.Json(errorResponse, statusCode: 404);
                }

                var successResponse = ApiResponse<ExerciseDetailResponse>.Success(
                    data: result.Value,
                    message: "Exercise details retrieved successfully.",
                    statusCode: 200
                );

                return Results.Json(successResponse, statusCode: 200);
            })
            .WithName("GetExerciseDetail")
            .WithTags("Exercises")
            .RequireAuthorization(); 
        }
    }
}
