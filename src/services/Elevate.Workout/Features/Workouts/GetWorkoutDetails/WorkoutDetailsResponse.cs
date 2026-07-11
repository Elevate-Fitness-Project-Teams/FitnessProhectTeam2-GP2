namespace Elevate.Workout.Features.Workouts.GetWorkoutDetails
{
    public record WorkoutDetailsResponse(
    int Id,
    string Name,
    string Category,
    string Difficulty,
    int Duration,
    List<WorkoutExerciseResponse> Exercises
);
}
