using Elevate.Nutrition.Domain.Enums;

namespace Elevate.Nutrition.Domain.Entities;

public class Meal : AggregateRoot<int>
{
    private string _tagsCsv = string.Empty;

    public string Name { get; private set; } = string.Empty;
    public string NutritionFacts { get; private set; } = string.Empty;
    public string Ingredients { get; private set; } = string.Empty;
    public string Instructions { get; private set; } = string.Empty;
    public int Calories { get; private set; }
    public int ProteinGrams { get; private set; }
    public MealType MealType { get; private set; }

    public IReadOnlyCollection<MealTag> Tags =>
        _tagsCsv.Split(',', StringSplitOptions.RemoveEmptyEntries)
                .Select(Enum.Parse<MealTag>)
                .ToList()
                .AsReadOnly();

    private Meal() { }

    public Meal(string name, string nutritionFacts, string ingredients,
                string instructions, int calories, int proteinGrams,
                MealType mealType, IEnumerable<MealTag> tags)
    {
        Name = name;
        NutritionFacts = nutritionFacts;
        Ingredients = ingredients;
        Instructions = instructions;
        Calories = calories;
        ProteinGrams = proteinGrams;
        MealType = mealType;
        UpdateTagsCsv(tags);
    }

    public void Update(string name, string nutritionFacts, string ingredients,
                       string instructions, int calories, int proteinGrams,
                       MealType mealType, IEnumerable<MealTag> tags)
    {
        Name = name;
        NutritionFacts = nutritionFacts;
        Ingredients = ingredients;
        Instructions = instructions;
        Calories = calories;
        ProteinGrams = proteinGrams;
        MealType = mealType;
        UpdateTagsCsv(tags);
    }

    public void AddTag(MealTag tag)
    {
        var current = Tags;
        if (!current.Contains(tag))
        {
            var updated = new List<MealTag>(current) { tag };
            UpdateTagsCsv(updated);
        }
    }

    public void RemoveTag(MealTag tag)
    {
        var current = Tags.ToList();
        if (current.Remove(tag))
        {
            UpdateTagsCsv(current);
        }
    }

    private void UpdateTagsCsv(IEnumerable<MealTag> tags)
    {
        _tagsCsv = string.Join(",", tags.Select(t => t.ToString()));
    }
}
