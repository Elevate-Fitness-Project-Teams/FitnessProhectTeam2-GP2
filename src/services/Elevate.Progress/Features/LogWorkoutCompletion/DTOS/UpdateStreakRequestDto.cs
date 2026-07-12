namespace Elevate.Progress.Features.LogWorkoutCompletion.DTOS
{
    public class UpdateStreakRequestDto
    {
        public Guid UserId { get; set; }
        public DateTime CompletedAt { get; set; }
    }
}
