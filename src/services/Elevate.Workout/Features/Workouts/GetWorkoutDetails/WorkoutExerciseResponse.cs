namespace Elevate.Workout.Features.Workouts.GetWorkoutDetails
{
    public record WorkoutExerciseResponse(
     int ExerciseId,
     string Name, 
     int OrderIndex
 );
}
