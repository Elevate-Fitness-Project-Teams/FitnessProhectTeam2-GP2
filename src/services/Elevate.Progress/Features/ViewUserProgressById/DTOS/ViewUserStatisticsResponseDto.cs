namespace Elevate.Progress.Features.ViewUserProgressById.DTOS
{
    public class ViewUserStatisticsResponseDto
    {
        public int TotalWorkouts { get; set; }

        public int TotalCaloriesBurned { get; set; }

        public decimal TotalWeightLost { get; set; }

        public int CurrentStreak { get; set; }

        public int LongestStreak { get; set; }
    }
}