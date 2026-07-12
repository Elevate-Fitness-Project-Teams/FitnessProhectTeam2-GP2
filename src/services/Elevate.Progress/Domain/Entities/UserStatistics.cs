namespace Elevate.Progress.Domain.Entities
{
    public class UserStatistics
    {
        public Guid Id { get; set; }

        public Guid UserId { get; set; }

        public int TotalWorkouts { get; set; }

        public int TotalCaloriesBurned { get; set; }

        public decimal TotalWeightLost { get; set; }

        public int CurrentStreak { get; set; }

        public int LongestStreak { get; set; }

        public DateTime UpdatedAt { get; set; } = DateTime.UtcNow;
        public int TotalWorkoutDuration { get; set; }
    }
}
