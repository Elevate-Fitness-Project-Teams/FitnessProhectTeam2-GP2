using Elevate.Progress.Domain.Enums;

namespace Elevate.Progress.Features.LogWorkoutCompletion.DTOS
{
    public class WorkoutSessionDto
    {
        public Guid SessionId { get; set; }

        public Guid UserId { get; set; }

        public WorkoutSessionStatus Status { get; set; }
    }
}
