namespace Elevate.Progress.Domain.Entities
{
    public class WorkoutLogExercises
    {
        public Guid Id { get; set; }

        public Guid WorkoutLogId { get; set; }

        public Guid ExerciseId { get; set; }
    }
}
