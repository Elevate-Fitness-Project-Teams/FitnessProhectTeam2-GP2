using MediatR;
using Microsoft.AspNetCore.Mvc;
using SharedKernel;

namespace Elevate.Workout.Features.Workouts.StartWorkoutSession
{
    public static class StartWorkoutSessionEndpoint
    {
        public record StartWorkoutSessionRequest(string Difficulty, int PlannedDuration);
        public static void MapStartWorkoutSessionEndpoint(this IEndpointRouteBuilder app)
        {
            app.MapPost("/api/v1/workouts/{id}/start", async (
                int id,
                [FromBody] StartWorkoutSessionRequest request,
                [FromServices] IMediator mediator) =>
            {
                var command = new StartWorkoutSessionCommand(id, request.Difficulty, request.PlannedDuration);

                var result = await mediator.Send(command);

                if (result.IsFailure)
                {
                    var errorResponse = ApiResponse<StartWorkoutSessionResponse>.Failure(
                        message: result.Error.Message,
                        statusCode: 404,
                        errors: [result.Error.Code]
                    );

                    return Results.Json(errorResponse, statusCode: 404);
                }

                var successResponse = ApiResponse<StartWorkoutSessionResponse>.Success(
                    data: result.Value,
                    message: "Workout session started successfully.",
                    statusCode: 201
                );

                return Results.Json(successResponse, statusCode: 201);
            })
            .WithName("StartWorkoutSession")
            .WithTags("Workouts")
            .RequireAuthorization();
        }
    }
}
    

