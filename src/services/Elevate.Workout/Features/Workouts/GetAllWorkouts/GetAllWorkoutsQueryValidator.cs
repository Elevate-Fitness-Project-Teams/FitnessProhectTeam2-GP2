using FluentValidation;

namespace Elevate.Workout.Features.Workouts.GetAllWorkouts
{
    public class GetAllWorkoutsQueryValidator : AbstractValidator<GetAllWorkoutsQuery>
    {
        private readonly HashSet<string> _allowedCategories = new()
    {
        "full-body", "chest", "arms", "shoulders", "back", "legs", "stomach"
    };

        public GetAllWorkoutsQueryValidator()
        {
            RuleFor(x => x.Category)
                .Must(cat => string.IsNullOrEmpty(cat) || _allowedCategories.Contains(cat.ToLower()))
                .WithErrorCode("VAL_REQUIRED_FIELD")
                .WithMessage("Invalid filter value for category.");

            RuleFor(x => x.Page).GreaterThan(0);
            RuleFor(x => x.PageSize).GreaterThan(0);
        }
    }
}
