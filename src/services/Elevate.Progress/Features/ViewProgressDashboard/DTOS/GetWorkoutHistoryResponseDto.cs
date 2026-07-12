namespace Elevate.Progress.Features.ViewProgressDashboard.DTOS
{
    public class GetWorkoutHistoryResponseDto
    {
        public List<WorkoutHistoryRecordDto> WorkoutHistory { get; set; } = [];
    }

    public class WorkoutHistoryRecordDto
    {
        public DateTime CompletedAt { get; set; }

        public int Duration { get; set; }

        public int CaloriesBurned { get; set; }
    }
}
