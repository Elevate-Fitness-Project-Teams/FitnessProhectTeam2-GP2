using Elevate.Workout.Domain.Enums;

namespace Elevate.Workout.Domain.Entities
{
    public sealed class WorkoutPlan
    {
        private WorkoutPlan() { }

        public int Id { get; private set; }
        public string Name { get; private set; } = string.Empty;
        public string Description { get; private set; } = string.Empty; 
        public DifficultyLevel Difficulty { get; private set; } 
        public int DurationWeeks { get; private set; }                 

        public ICollection<Workout> Workouts { get; private set; } = new List<Workout>();

        public static WorkoutPlan Create(string name, string description, string difficulty, int durationWeeks)
        {
            var difficultyLevel = Enum.Parse<DifficultyLevel>(difficulty, ignoreCase: true);
            return new WorkoutPlan
            {

                Name = name,
                Description = description,
                Difficulty = difficultyLevel,
                DurationWeeks = durationWeeks
            };
        }
    }
}
