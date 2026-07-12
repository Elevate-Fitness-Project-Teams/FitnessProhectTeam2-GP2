using Elevate.Progress.Domain.Enums;

namespace Elevate.Progress.Domain.Entities
{
    public class WorkoutLog
    {
        public Guid Id { get; set; }

        public Guid UserId { get; set; }

        public Guid WorkoutId { get; set; }

        public Guid WorkoutSessionId { get; set; }

        public DateTime CompletedAt { get; set; }

        public int Duration { get; set; }

        public int CaloriesBurned { get; set; }

        public WorkoutDifficulty Difficulty { get; set; }

        public string? Notes { get; set; }

        public int Rating { get; set; }
    }
}
