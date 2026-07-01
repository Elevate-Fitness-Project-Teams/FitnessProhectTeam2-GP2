using FluentValidation;

namespace Elevate.Nutrition.Application.Features.MealPlans.Commands.AddMealPlanItem;

public class AddMealPlanItemCommandValidator : AbstractValidator<AddMealPlanItemCommand>
{
    public AddMealPlanItemCommandValidator()
    {
        RuleFor(x => x.MealPlanId).GreaterThan(0);
        RuleFor(x => x.MealId).GreaterThan(0);
        RuleFor(x => x.ServingCount).GreaterThan(0);
    }
}
