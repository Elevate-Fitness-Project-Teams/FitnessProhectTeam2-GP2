namespace Elevate.Progress.Features.ViewProgressDashboard.DTOS
{
    public class ViewProgressDashboardResponseDto
    {
        public GetUserStatisticsResponseDto SummaryStatistics { get; set; } = new();

        public GetWeightHistoryResponseDto WeightHistory { get; set; } = new();

        public GetWorkoutHistoryResponseDto WorkoutHistory { get; set; } = new();

        public GetWeekComparisonResponseDto WeekComparison { get; set; } = new();

        public GetEarnedAchievementsResponseDto EarnedAchievements { get; set; } = new();
    }
}
