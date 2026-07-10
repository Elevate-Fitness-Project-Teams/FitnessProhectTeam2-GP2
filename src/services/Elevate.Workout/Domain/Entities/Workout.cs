using Elevate.Workout.Domain.Enums;
using System.ComponentModel.DataAnnotations.Schema;

namespace Elevate.Workout.Domain.Entities
{
    public class Workout
    {
        private readonly List<WorkoutExerciseCatalogItem> _exercises = new();

        public int WorkoutId { get; set; }
        public string Name { get; set; } = string.Empty;
        public WorkoutCategory Category { get; set; }
        public DifficultyLevel Difficulty { get; set; }
        public int EstimatedDurationInMinutes { get; set; }
        public int OrderIndex { get; set; }
        public int WorkoutPlanId { get; set; }

        [ForeignKey(nameof(WorkoutPlanId))]
        public WorkoutPlan WorkoutPlan { get; set; } = null!;

        public IReadOnlyCollection<WorkoutExerciseCatalogItem> Exercises => _exercises.AsReadOnly();

        public void AddExercise(int exerciseId, int orderIndex)
        {
            _exercises.Add(WorkoutExerciseCatalogItem.Create(WorkoutId, exerciseId, orderIndex));
        }
    }
}
