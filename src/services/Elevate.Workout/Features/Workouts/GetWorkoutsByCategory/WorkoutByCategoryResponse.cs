namespace Elevate.Workout.Features.Workouts.GetWorkoutsByCategory
{
    public record WorkoutByCategoryResponse(
     int WorkoutId,
     string Name,
     string Category,
     string Difficulty,
     int EstimatedDurationInMinutes,
     int OrderIndex
 );
}
