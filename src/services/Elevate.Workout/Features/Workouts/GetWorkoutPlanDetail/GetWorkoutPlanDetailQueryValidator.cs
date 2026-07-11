using FluentValidation;

namespace Elevate.Workout.Features.Workouts.GetWorkoutPlanDetail
{
    public sealed class GetWorkoutPlanDetailQueryValidator : AbstractValidator<GetWorkoutPlanDetailQuery>
    {
        public GetWorkoutPlanDetailQueryValidator()
        {
            RuleFor(x => x.PlanId).GreaterThan(0);
        }
    }
}
