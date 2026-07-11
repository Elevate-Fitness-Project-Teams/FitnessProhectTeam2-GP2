namespace Elevate.Workout.Domain.Entities
{
    public sealed class WorkoutExerciseCatalogItem
    {
        private WorkoutExerciseCatalogItem() { }

        public int Id { get; private set; }
        public int WorkoutId { get; private set; }
        public int ExerciseId { get; private set; }
        public int OrderIndex { get; private set; }

        public Exercise Exercise { get; private set; } = null!;

        public static WorkoutExerciseCatalogItem Create(int workoutId, int exerciseId, int orderIndex)
        {
            return new WorkoutExerciseCatalogItem
            {
                WorkoutId = workoutId,
                ExerciseId = exerciseId,
                OrderIndex = orderIndex
            };
        }
    }
}
