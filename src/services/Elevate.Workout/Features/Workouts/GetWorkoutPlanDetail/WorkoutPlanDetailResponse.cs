namespace Elevate.Workout.Features.Workouts.GetWorkoutPlanDetail
{
    public sealed record WorkoutPlanDetailResponse(
    Guid PlanId,
    string Name,
    string Description,
    string Difficulty,
    int DurationWeeks);
}
