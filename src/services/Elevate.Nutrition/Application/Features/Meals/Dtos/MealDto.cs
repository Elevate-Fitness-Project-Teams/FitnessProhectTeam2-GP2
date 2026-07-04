using Elevate.Nutrition.Domain.Entities;

namespace Elevate.Nutrition.Application.Features.Meals.Dtos;

public class MealDto
{
    public int Id { get; init; }
    public string Name { get; init; } = string.Empty;
    public string NutritionFacts { get; init; } = string.Empty;
    public string Ingredients { get; init; } = string.Empty;
    public string Instructions { get; init; } = string.Empty;
    public int Calories { get; init; }
    public string MealType { get; init; } = string.Empty;
    public List<string> Tags { get; init; } = new();
    public DateTime CreatedAt { get; init; }
    public DateTime UpdatedAt { get; init; }

    public static MealDto FromEntity(Meal meal) => new()
    {
        Id = meal.Id,
        Name = meal.Name,
        NutritionFacts = meal.NutritionFacts,
        Ingredients = meal.Ingredients,
        Instructions = meal.Instructions,
        Calories = meal.Calories,
        MealType = meal.MealType.ToString(),
        Tags = meal.Tags.Select(t => t.ToString()).ToList(),
        CreatedAt = meal.CreatedAt,
        UpdatedAt = meal.UpdatedAt
    };
}
