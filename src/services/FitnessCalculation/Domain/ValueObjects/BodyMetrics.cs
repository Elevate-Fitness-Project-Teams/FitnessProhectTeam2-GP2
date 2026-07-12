using Elevate.FitnessCalculation.Domain.Enums;

namespace Elevate.FitnessCalculation.Domain.ValueObjects
{
    public sealed record BodyMetrics
    {
        public double Weight { get; }

        public double Height { get; }

        public int Age { get; }

       

        private BodyMetrics(
            double weight,
            double height,
            int age
            )
        {
            if (weight <= 0)
                throw new ArgumentOutOfRangeException(nameof(weight));

            if (height <= 0)
                throw new ArgumentOutOfRangeException(nameof(height));

            if (age <= 0)
                throw new ArgumentOutOfRangeException(nameof(age));

            Weight = weight;
            Height = height;
            Age = age;
          
        }

        public static BodyMetrics Create(
            double weight,
            double height,
            int age
            )
        {
            return new BodyMetrics(weight, height, age);
        }
    }
}