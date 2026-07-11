namespace Elevate.FitnessCalculation.Domain.ValueObjects
{
    public sealed record CaloriesRange
    {
        public double MinCalorie { get; }
        public double MaxCalorie { get; }

        private CaloriesRange(double minCalorie, double maxCalorie)
        {
            if (minCalorie < 0)
                throw new ArgumentException("Minimum calorie cannot be negative.");
            if (maxCalorie < 0)
                throw new ArgumentException("Maximum calorie cannot be negative.");
            if (minCalorie > maxCalorie)
                throw new ArgumentException("Minimum calorie cannot be greater than maximum calorie.");
            MinCalorie = minCalorie;
            MaxCalorie = maxCalorie;
        }

        public static CaloriesRange create(double min,double max)
        {
            return new(min, max);
        }
    }
}
