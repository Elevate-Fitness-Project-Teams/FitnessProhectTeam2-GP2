namespace Elevate.Workout.Features.Workouts.GetWorkoutPlanDetail
{
    public sealed record WorkoutPlanDetailResponse(
    int PlanId,
    string Name,
    string Description,
    string Difficulty,
    int DurationWeeks);
}
