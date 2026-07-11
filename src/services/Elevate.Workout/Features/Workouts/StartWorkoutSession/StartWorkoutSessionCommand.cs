using MediatR;
using SharedKernel;

namespace Elevate.Workout.Features.Workouts.StartWorkoutSession
{
    public record StartWorkoutSessionCommand(
    int WorkoutId,
    string Difficulty,
    int PlannedDuration) : IRequest<Result<StartWorkoutSessionResponse>>;
}
