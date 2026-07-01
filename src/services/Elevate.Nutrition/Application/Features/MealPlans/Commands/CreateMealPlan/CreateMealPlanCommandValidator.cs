using FluentValidation;

namespace Elevate.Nutrition.Application.Features.MealPlans.Commands.CreateMealPlan;

public class CreateMealPlanCommandValidator : AbstractValidator<CreateMealPlanCommand>
{
    public CreateMealPlanCommandValidator()
    {
        RuleFor(x => x.TargetCalorieRangeMin).GreaterThanOrEqualTo(0);
        RuleFor(x => x.TargetCalorieRangeMax)
            .GreaterThan(x => x.TargetCalorieRangeMin)
            .WithMessage("Max calories must be greater than min calories");
        RuleFor(x => x.Goal).IsInEnum();
    }
}
