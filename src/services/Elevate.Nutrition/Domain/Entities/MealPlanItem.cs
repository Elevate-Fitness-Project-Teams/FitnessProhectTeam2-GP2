using Elevate.Nutrition.Domain.Common;

namespace Elevate.Nutrition.Domain.Entities;

public class MealPlanItem : BaseEntity<int>
{
    public int MealPlanId { get; private set; }
    public MealPlan MealPlan { get; private set; } = null!;
    public int MealId { get; private set; }
    public Meal Meal { get; private set; } = null!;
    public int ServingCount { get; private set; }

    private MealPlanItem() { }

    public MealPlanItem(int mealId, int servingCount)
    {
        MealId = mealId;
        ServingCount = servingCount;
    }

    public void UpdateServingCount(int servingCount) => ServingCount = servingCount;
}
