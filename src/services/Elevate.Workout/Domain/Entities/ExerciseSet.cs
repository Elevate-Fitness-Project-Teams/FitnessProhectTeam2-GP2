namespace Elevate.Workout.Domain.Entities
{
    public sealed class ExerciseSet
    {
        private ExerciseSet() { }

        public int Id { get; private set; } 
        public int WorkoutExerciseId { get; private set; } 
        public int SetNumber { get; private set; }
        public int Reps { get; private set; }
        public decimal WeightInKg { get; private set; }
        public bool IsCompleted { get; private set; }

        public static ExerciseSet Create(int workoutExerciseId, int setNumber, int reps, decimal weightInKg)
        {
            return new ExerciseSet
            {
                WorkoutExerciseId = workoutExerciseId,
                SetNumber = setNumber,
                Reps = reps,
                WeightInKg = weightInKg,
                IsCompleted = false
            };
        }

        public void Complete()
        {
            IsCompleted = true;
        }
    }
}
