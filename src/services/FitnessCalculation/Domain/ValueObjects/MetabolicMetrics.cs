using System.Runtime.InteropServices;

namespace Elevate.FitnessCalculation.Domain.ValueObjects
{
    public sealed record MetabolicMetrics
    {
        public double Bmr { get; }
        public double Tdee { get; }
        public double CalorieTarget { get; }

        private MetabolicMetrics(
            double bmr,
            double tdee,
            double calorieTarget)
        {
            if (bmr <= 0)
                throw new ArgumentException("Invalid BMR.");

            if (tdee <= 0)
                throw new ArgumentException("Invalid TDEE.");

            if (calorieTarget <= 0)
                throw new ArgumentException("Invalid calorie target.");

            Bmr = bmr;
            Tdee = tdee;
            CalorieTarget = calorieTarget;
        }

        public static MetabolicMetrics Create(double bmr,double tdee,double calorieTarget)
        {
            return new MetabolicMetrics(bmr,tdee,calorieTarget);
        }
    }
}