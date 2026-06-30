using Elevate.Nutrition.Domain.Enums;
using Elevate.Nutrition.Domain.ValueObjects;

namespace Elevate.Nutrition.Domain.Entities;

public class MealPlan : AggregateRoot<int>
{
    public int TargetCalorieRangeMin { get; private set; }
    public int TargetCalorieRangeMax { get; private set; }
    public MealPlanGoal Goal { get; private set; }
    public MealPlanStatus Status { get; private set; }

    private readonly List<MealPlanItem> _items = new();
    public IReadOnlyCollection<MealPlanItem> Items => _items.AsReadOnly();

    private MealPlan() { }

    public MealPlan(int targetCalorieRangeMin, int targetCalorieRangeMax,
                    MealPlanGoal goal)
    {
        var range = new CalorieRange(targetCalorieRangeMin, targetCalorieRangeMax);
        TargetCalorieRangeMin = range.Min;
        TargetCalorieRangeMax = range.Max;
        Goal = goal;
        Status = MealPlanStatus.Draft;
    }

    public void UpdateCalorieRange(int min, int max)
    {
        var range = new CalorieRange(min, max);
        TargetCalorieRangeMin = range.Min;
        TargetCalorieRangeMax = range.Max;
    }

    public void UpdateGoal(MealPlanGoal goal) => Goal = goal;
    public void Activate() => Status = MealPlanStatus.Active;
    public void Complete() => Status = MealPlanStatus.Completed;
    public void Archive() => Status = MealPlanStatus.Archived;

    public void AddItem(MealPlanItem item) => _items.Add(item);
    public void RemoveItem(MealPlanItem item) => _items.Remove(item);
}
