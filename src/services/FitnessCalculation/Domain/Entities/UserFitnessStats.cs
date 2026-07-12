using Elevate.FitnessCalculation.Domain.Enums;
using Elevate.FitnessCalculation.Domain.Exceptions;
using Elevate.FitnessCalculation.Domain.ValueObjects;

namespace Elevate.FitnessCalculation.Domain.Entities
{
    public class UserFitnessStats
    {
        public int Id { get; private set; }

        public int UserId { get; private set; }

        public BodyMetrics BodyMetrics { get; set; }

        public Gender Gender { get; private set; }

        public Goal Goal { get; private set; }

        public ActivityLevel ActivityLevel { get; private set; }

        public DateTime RecordedAt { get; private set; }
        // Navigation Property
        public CalculatedMetrics? CalculatedMetrics { get; set; }

        private UserFitnessStats() { } // Required by EF Core

        private UserFitnessStats(
            int userId,
          BodyMetrics bodyMetrics,
            Gender gender,
            Goal goal,
            ActivityLevel activityLevel)
        {
            if (!Enum.IsDefined(gender))
                throw new DomainValidException("VAL_INVALID_GENDER");

            if (!Enum.IsDefined(goal))
                throw new DomainValidException("VAL_INVALID_GOAL");

            if (!Enum.IsDefined(activityLevel))
                throw new DomainValidException("VAL_INVALID_ACTIVITY");
            UserId = userId;
            BodyMetrics = bodyMetrics;
            Gender = gender;
            Goal = goal;
            ActivityLevel = activityLevel;
            RecordedAt = DateTime.UtcNow;
        }

        public static UserFitnessStats Create(
            int userId,
            BodyMetrics bodyMetrics,
            Gender gender,
            Goal goal,
            ActivityLevel activityLevel)
        {
            return new UserFitnessStats(
                userId,
                bodyMetrics,
                gender,
                goal,
                activityLevel);
        }

        //public static 

    } 
}