using Elevate.Workout.Domain.Enums;

namespace Elevate.Workout.Features.Workouts.GetAllWorkouts
{
    public record WorkoutResponse(
    int Id,
    string Name,
    WorkoutCategory Category,
    DifficultyLevel Difficulty,
    int Duration,
    int OrderIndex,
    string? PlanName 
    );
}
