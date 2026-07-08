using FluentValidation;

namespace Elevate.Nutrition.Application.Features.MealPlans.Commands.CreateMealPlanFromTarget;

public class CreateMealPlanFromTargetCommandValidator : AbstractValidator<CreateMealPlanFromTargetCommand>
{
    public CreateMealPlanFromTargetCommandValidator()
    {
        RuleFor(x => x.UserId).GreaterThan(0);
        RuleFor(x => x.Goal).IsInEnum();
    }
}
