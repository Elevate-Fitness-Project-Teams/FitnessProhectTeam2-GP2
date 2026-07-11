using Elevate.Workout.Domain.Enums;

namespace Elevate.Workout.Domain.Entities
{
    public class WorkoutSession
    {
        private readonly List<WorkoutExercise> _exercises = new();

        private WorkoutSession() { }

        public Guid Id { get; private set; }
        public int WorkoutID { get; private set; }
        public Guid UserId { get; private set; }
        public string Name { get; private set; } = string.Empty;
        public SessionStatus Type { get; private set; }
        public int DurationInMinutes { get; private set; }
        public int CaloriesBurned { get; private set; }
        public DateTime CreatedAt { get; private set; }

        public IReadOnlyCollection<WorkoutExercise> Exercises => _exercises.AsReadOnly();

        public static WorkoutSession Create(Guid userId, string name, SessionStatus type, int duration, int calories, DateTime createdAt, int workoutId)
        {
            return new WorkoutSession
            {
                Id = Guid.NewGuid(),
                UserId = userId,
                Name = name,
                Type = type,
                DurationInMinutes = duration,
                CaloriesBurned = calories,
                CreatedAt = createdAt,
                WorkoutID = workoutId
            };
        }

        public void AddExercise(int exerciseId, int order)
        {
            var exercise = WorkoutExercise.Create(this.Id, exerciseId, order);
            _exercises.Add(exercise);
        }
    }
}
