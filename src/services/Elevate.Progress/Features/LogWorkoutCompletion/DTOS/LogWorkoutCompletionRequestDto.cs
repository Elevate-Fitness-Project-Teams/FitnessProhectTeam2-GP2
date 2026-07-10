using Elevate.Progress.Domain.Enums;

namespace Elevate.Progress.Features.LogWorkoutCompletion.DTOS
{
    public class LogWorkoutCompletionRequestDto
    {
        public Guid WorkoutSessionId { get; set; }
        public Guid UserId { get; set; }

        public Guid WorkoutId { get; set; }

        public DateTime CompletedAt { get; set; }

        public int Duration { get; set; }

        public int CaloriesBurned { get; set; }

        public WorkoutDifficulty Difficulty { get; set; }

        public string? Notes { get; set; }

        public int Rating { get; set; }

        public List<WorkoutLogExercisesDto> Exercises { get; set; } = [];
    }
}
