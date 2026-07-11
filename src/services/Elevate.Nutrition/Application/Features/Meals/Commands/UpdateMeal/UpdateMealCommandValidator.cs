using FluentValidation;

namespace Elevate.Nutrition.Application.Features.Meals.Commands.UpdateMeal;

public class UpdateMealCommandValidator : AbstractValidator<UpdateMealCommand>
{
    public UpdateMealCommandValidator()
    {
        RuleFor(x => x.Id).GreaterThan(0);
        RuleFor(x => x.Name).NotEmpty().MaximumLength(200);
        RuleFor(x => x.Calories).GreaterThan(0);
        RuleFor(x => x.ProteinGrams).GreaterThanOrEqualTo(0);
        RuleFor(x => x.Ingredients).NotEmpty().MaximumLength(4000);
        RuleFor(x => x.NutritionFacts).MaximumLength(2000);
        RuleFor(x => x.Instructions).MaximumLength(4000);
        RuleFor(x => x.MealType).IsInEnum();
    }
}
