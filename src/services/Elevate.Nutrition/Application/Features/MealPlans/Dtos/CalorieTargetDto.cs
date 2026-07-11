namespace Elevate.Nutrition.Application.Features.MealPlans.Dtos;

public class CalorieTargetDto
{
    public int UserId { get; init; }
    public int TargetCalories { get; init; }
    public DateTime CalculatedAt { get; init; }
}
