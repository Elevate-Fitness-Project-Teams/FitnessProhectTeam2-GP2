namespace Elevate.Progress.Features.LogWorkoutCompletion.DTOS
{
    public class CompleteWorkoutSessionRequestDto
    {
        public Guid WorkoutSessionId { get; set; }
        public DateTime CompletedAt { get; set; }
    }
}
