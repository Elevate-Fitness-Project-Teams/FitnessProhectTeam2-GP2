namespace Elevate.Workout.Domain.Entities
{
    public sealed class WorkoutExercise
    {
        private readonly List<ExerciseSet> _sets = new();

        private WorkoutExercise() { }

        public int Id { get; private set; }

        public Guid WorkoutSessionId { get; private set; }
        public string ExerciseName { get; private set; } = string.Empty;
        public int Order { get; private set; }
        public int ExerciseId { get; private set; }
        public Exercise Exercise { get; private set; } = null!;
        public IReadOnlyCollection<ExerciseSet> Sets => _sets.AsReadOnly();
        public static WorkoutExercise Create(Guid workoutSessionId, int exerciseId, int order)
        {
            return new WorkoutExercise
            {
                WorkoutSessionId = workoutSessionId,
                ExerciseId = exerciseId,
                Order = order
            };
        }

        public void AddSet(int setNumber, int reps, decimal weightInKg)
        {
            var set = ExerciseSet.Create(this.Id, setNumber, reps, weightInKg);
            _sets.Add(set);
        }
    }
}