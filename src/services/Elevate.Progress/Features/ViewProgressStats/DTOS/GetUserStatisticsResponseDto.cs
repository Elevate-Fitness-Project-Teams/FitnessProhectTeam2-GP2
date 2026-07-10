namespace Elevate.Progress.Features.ViewProgressStats.DTOS
{
    public class GetUserStatisticsResponseDto
    {
        public int TotalWorkouts { get; set; }

        public int TotalCaloriesBurned { get; set; }

        public decimal TotalWeightLost { get; set; }
    }
}
