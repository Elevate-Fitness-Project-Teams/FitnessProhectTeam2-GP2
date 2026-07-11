using FluentValidation;

namespace Elevate.Nutrition.Application.Features.MealPlans.Commands.RemoveMealPlanItem;

public class RemoveMealPlanItemCommandValidator : AbstractValidator<RemoveMealPlanItemCommand>
{
    public RemoveMealPlanItemCommandValidator()
    {
        RuleFor(x => x.MealPlanId).GreaterThan(0);
        RuleFor(x => x.MealPlanItemId).GreaterThan(0);
    }
}
