namespace Elevate.Fitness.Models;

public class CalorieTarget
{
    public int UserId { get; init; }
    public int TargetCalories { get; init; }
    public DateTime CalculatedAt { get; init; }
}
