using Elevate.FitnessCalculation.Domain.Enums;
using Elevate.FitnessCalculation.Domain.Exceptions;

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
            if (weight <40 || weight>250)
                throw new DomainValidException("VAL_INVALID_Weight");

            if (height < 140 || height > 220)
                throw new DomainValidException("VAL_INVALID_HEIGHT");

            if (age < 16 || age > 100)
                throw new DomainValidException("VAL_INVALID_AGE");

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