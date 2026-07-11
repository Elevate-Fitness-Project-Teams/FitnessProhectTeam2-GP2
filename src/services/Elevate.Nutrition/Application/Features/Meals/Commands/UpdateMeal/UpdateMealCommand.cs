using MediatR;
using Elevate.Nutrition.Domain.Common;
using Elevate.Nutrition.Domain.Enums;

namespace Elevate.Nutrition.Application.Features.Meals.Commands.UpdateMeal;

public record UpdateMealCommand(
    int Id,
    string Name,
    string NutritionFacts,
    string Ingredients,
    string Instructions,
    int Calories,
    int ProteinGrams,
    MealType MealType,
    IEnumerable<MealTag> Tags
) : IRequest<Result>;
