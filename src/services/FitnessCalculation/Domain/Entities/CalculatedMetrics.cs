using Elevate.FitnessCalculation.Domain.Entities;
using Elevate.FitnessCalculation.Domain.Enums;
using Elevate.FitnessCalculation.Domain.ValueObjects;

public class CalculatedMetrics
{
        public int Id { get; private set; }

        public int UserId { get; private set; }

        public MetabolicMetrics Metabolic { get; private set; }

        public Status Status { get; private set; }

        public DateTime CalculatedAt { get; private set; }

        public UserFitnessStats UserFitnessStats { get; private set; } = null!;

        private CalculatedMetrics() { } // EF

        private CalculatedMetrics(
            int userId,
            MetabolicMetrics metabolic,
            Status status)
        {
            UserId = userId;
            Metabolic = metabolic;
            Status = status;
            CalculatedAt = DateTime.UtcNow;
        }

        public static CalculatedMetrics Create(
            int userId,
            MetabolicMetrics metabolic,
            Status status)
        {
            return new CalculatedMetrics(userId, metabolic, status);
        }

        public void Update(
            MetabolicMetrics metabolic,
            Status status)
        {
            Metabolic = metabolic;
            Status = status;
            CalculatedAt = DateTime.UtcNow;
        }
}