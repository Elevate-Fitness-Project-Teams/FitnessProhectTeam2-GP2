using Elevate.Progress.Shared.Exceptions;

namespace Elevate.Progress.Features.Exception
{
    public class NotFoundWorkoutSession : NotFoundException
    {
        public NotFoundWorkoutSession(Guid sessionId)
            : base(
                "RES_SESSION_NOT_FOUND",
                $"Workout session with id {sessionId} not found.")
        {
        }
    }
}
