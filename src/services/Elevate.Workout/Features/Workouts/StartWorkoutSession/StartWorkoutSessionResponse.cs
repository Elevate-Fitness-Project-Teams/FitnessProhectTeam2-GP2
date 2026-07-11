namespace Elevate.Workout.Features.Workouts.StartWorkoutSession
{
    public record StartWorkoutSessionResponse(Guid SessionId,
        List<SessionExerciseResponse> Exercises);
}
