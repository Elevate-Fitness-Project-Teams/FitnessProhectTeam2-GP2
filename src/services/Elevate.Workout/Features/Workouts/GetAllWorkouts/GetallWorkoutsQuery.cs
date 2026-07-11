using MediatR;
using SharedKernel;
using SharedKernel.Pagniation;

namespace Elevate.Workout.Features.Workouts.GetAllWorkouts
{
    public record GetAllWorkoutsQuery(
    int Page = 1,
    int PageSize = 10,
    string? Category = null,
    string? Difficulty = null,
    int? Duration = null,
    string? Search = null) : IRequest<Result<PagedList<WorkoutResponse>>>;
}
