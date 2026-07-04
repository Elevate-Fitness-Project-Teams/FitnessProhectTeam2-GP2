using Elevate.Nutrition.Domain.Entities;

namespace Elevate.Nutrition.Application.Features.MealPlans.Dtos;

public class MealPlanItemDto
{
    public int Id { get; init; }
    public int MealId { get; init; }
    public string MealName { get; init; } = string.Empty;
    public int MealCalories { get; init; }
    public int ServingCount { get; init; }
    public int TotalCalories => MealCalories * ServingCount;

    public static MealPlanItemDto FromEntity(MealPlanItem item) => new()
    {
        Id = item.Id,
        MealId = item.Meal.Id,
        MealName = item.Meal.Name,
        MealCalories = item.Meal.Calories,
        ServingCount = item.ServingCount
    };
}
