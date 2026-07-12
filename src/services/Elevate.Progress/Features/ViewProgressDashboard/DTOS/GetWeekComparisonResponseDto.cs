namespace Elevate.Progress.Features.ViewProgressDashboard.DTOS
{
    public class GetWeekComparisonResponseDto
    {
        public int WorkoutDifference { get; set; }

        public int CaloriesDifference { get; set; }

        public decimal WeightDifference { get; set; }
    }
}
