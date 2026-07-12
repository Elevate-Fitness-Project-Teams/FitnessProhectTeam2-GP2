namespace Elevate.Progress.Features.ViewProgressStats.DTOS
{
    public class ViewProgressStatsResponseDto
    {
        public int TotalWorkouts { get; set; }

        public int TotalCaloriesBurned { get; set; }

        public decimal TotalWeightLost { get; set; }

        public int CurrentStreak { get; set; }

        public int LongestStreak { get; set; }
    }
}