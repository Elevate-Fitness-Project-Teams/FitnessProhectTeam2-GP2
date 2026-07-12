using Elevate.Progress.Shared.Exceptions;

namespace Elevate.Progress.Features.LogWorkoutCompletion.Exception
{
    public class WorkoutSessionAlreadyCompletedException : BadRequestException
    {
        public WorkoutSessionAlreadyCompletedException(Guid sessionId)
            : base(
                "RES_WORKOUT_SESSION_ALREADY_COMPLETED",
                $"Workout session with id {sessionId} is already completed.")
        {
        }
    }
}
