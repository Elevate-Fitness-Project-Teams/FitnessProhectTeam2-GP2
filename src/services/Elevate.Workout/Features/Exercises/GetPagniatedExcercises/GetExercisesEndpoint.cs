using MediatR;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Routing;
using SharedKernel;
using SharedKernel.Pagniation;

namespace Elevate.Workout.Features.Exercises.GetPagniatedExcercise;

public static class GetExercisesEndpoint
{
    public static void MapGetExercisesEndpoint(this IEndpointRouteBuilder app)
    {
        app.MapGet("/api/v1/exercises", async (
            [FromQuery] int pageNumber,
            [FromQuery] int pageSize,
            [FromServices] IMediator mediator) =>
        {
            
            var query = new GetExercisesQuery(pageNumber,pageSize);

            var result = await mediator.Send(query);

            var successResponse = ApiResponse<PagedList<ExerciseResponse>>.Success(
                data: result.Value,
                message: "Exercises library retrieved successfully.",
                statusCode: 200
            );

            return Results.Json(successResponse, statusCode: 200);
        })
        .WithName("GetExercises")
        .WithTags("Exercises")
        .RequireAuthorization();
    }
}