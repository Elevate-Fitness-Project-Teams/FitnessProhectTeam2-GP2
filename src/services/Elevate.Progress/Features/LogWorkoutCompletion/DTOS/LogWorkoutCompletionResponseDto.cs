namespace Elevate.Progress.Features.LogWorkoutCompletion.DTOS
{
    public class LogWorkoutCompletionResponseDto
    {
        public Guid WorkoutLogId { get; set; }

        public int CurrentStreak { get; set; }

        public bool Success { get; set; }
    }
}
