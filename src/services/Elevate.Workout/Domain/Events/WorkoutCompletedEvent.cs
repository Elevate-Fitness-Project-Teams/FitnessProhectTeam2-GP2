using SharedKernel.Interfaces;

namespace Elevate.Workout.Domain.Events
{
    public record WorkoutCompletedEvent(
    Guid SessionId,
    Guid UserId,
    int CaloriesBurned,
    int CurrentStreak,
    DateTime OccurredAt) : IDomainEvent;
}
