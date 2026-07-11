using Elevate.FitnessCalculation.Domain.Enums;

namespace Elevate.FitnessCalculation.Domain.Entities
{
    public class UserFitnessStats
    {
        public int Id { get; private set; }

        public int UserId { get; private set; }

        public double Weight { get; private set; }

        public double Height { get; private set; }

        public int Age { get; private set; }

        public Gender Gender { get; private set; }

        public Goal Goal { get; private set; }

        public ActivityLevel ActivityLevel { get; private set; }

        public DateTime RecordedAt { get; private set; }

        private UserFitnessStats() { } // Required by EF Core

        private UserFitnessStats(
            int userId,
            double weight,
            double height,
            int age,
            Gender gender,
            Goal goal,
            ActivityLevel activityLevel)
        {
           

            UserId = userId;
            Weight = weight;
            Height = height;
            Age = age;
            Gender = gender;
            Goal = goal;
            ActivityLevel = activityLevel;
            RecordedAt = DateTime.UtcNow;
        }

        public static UserFitnessStats Create(
            int userId,
            double weight,
            double height,
            int age,
            Gender gender,
            Goal goal,
            ActivityLevel activityLevel)
        {
            return new UserFitnessStats(
                userId,
                weight,
                height,
                age,
                gender,
                goal,
                activityLevel);
        }


    } 
}