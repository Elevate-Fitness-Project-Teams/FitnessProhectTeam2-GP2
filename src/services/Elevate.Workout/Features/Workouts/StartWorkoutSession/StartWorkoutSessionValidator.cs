using FluentValidation;

namespace Elevate.Workout.Features.Workouts.StartWorkoutSession
{
    public sealed class StartWorkoutSessionValidator : AbstractValidator<StartWorkoutSessionCommand>
    {
        public StartWorkoutSessionValidator()
        {
            RuleFor(x => x.WorkoutId)
                .GreaterThan(0)
                .WithMessage("Workout ID must be a valid positive integer.");

            RuleFor(x => x.Difficulty)
                .NotEmpty()
                .WithMessage("Difficulty level is required.");

            RuleFor(x => x.PlannedDuration)
                .GreaterThan(0)
                .WithMessage("Planned duration must be at least 1 minute.")
                .LessThanOrEqualTo(180)
                .WithMessage("Planned duration cannot exceed 180 minutes.");
        }
    }
}
