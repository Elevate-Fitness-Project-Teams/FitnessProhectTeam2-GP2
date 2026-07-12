namespace Elevate.Progress.Domain.Entities
{
    public class Streak
    {
        public Guid Id { get; set; }

        public Guid UserId { get; set; }

        public int CurrentStreak { get; set; }

        public int LongestStreak { get; set; }

        public DateTime LastWorkoutDate { get; set; }
    }
}
