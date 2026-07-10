namespace Elevate.Workout.Features.Workouts.GetWorkoutsByPlan
{
    public record WorkoutByPlanResponse(
    int WorkoutId,
    string Name,
    string Category,
    string Difficulty,
    int EstimatedDurationInMinutes,
    int OrderIndex
);
}
