using Elevate.Nutrition.Domain.Entities;

namespace Elevate.Nutrition.Application.Features.MealPlans.Dtos;

public class MealPlanDto
{
    public int Id { get; init; }
    public int TargetCalorieRangeMin { get; init; }
    public int TargetCalorieRangeMax { get; init; }
    public string Goal { get; init; } = string.Empty;
    public string Status { get; init; } = string.Empty;
    public List<MealPlanItemDto> Items { get; init; } = new();
    public int TotalCalories => Items.Sum(i => i.TotalCalories);
    public DateTime CreatedAt { get; init; }
    public DateTime UpdatedAt { get; init; }

    public static MealPlanDto FromEntity(MealPlan mealPlan) => new()
    {
        Id = mealPlan.Id,
        TargetCalorieRangeMin = mealPlan.TargetCalorieRangeMin,
        TargetCalorieRangeMax = mealPlan.TargetCalorieRangeMax,
        Goal = mealPlan.Goal.ToString(),
        Status = mealPlan.Status.ToString(),
        Items = mealPlan.Items.Select(MealPlanItemDto.FromEntity).ToList(),
        CreatedAt = mealPlan.CreatedAt,
        UpdatedAt = mealPlan.UpdatedAt
    };
}
