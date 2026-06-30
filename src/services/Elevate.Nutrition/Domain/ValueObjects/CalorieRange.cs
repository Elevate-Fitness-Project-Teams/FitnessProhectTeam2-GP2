namespace Elevate.Nutrition.Domain.ValueObjects;

public class CalorieRange
{
    public int Min { get; }
    public int Max { get; }

    public CalorieRange(int min, int max)
    {
        if (min < 0)
            throw new ArgumentException("Min calories cannot be negative", nameof(min));
        if (max <= min)
            throw new ArgumentException("Max must be greater than Min", nameof(max));
        Min = min;
        Max = max;
    }

    public bool Contains(int calories) => calories >= Min && calories <= Max;
}
