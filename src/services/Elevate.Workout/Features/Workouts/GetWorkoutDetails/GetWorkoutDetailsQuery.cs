using MediatR;
using SharedKernel;

namespace Elevate.Workout.Features.Workouts.GetWorkoutDetails
{
    public record GetWorkoutDetailsQuery(int Id) : IRequest<Result<WorkoutDetailsResponse>>;
}
