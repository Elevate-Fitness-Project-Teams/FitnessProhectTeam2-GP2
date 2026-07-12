using Elevate.FitnessCalculation.Domain.Enums;
using Elevate.FitnessCalculation.Domain.ValueObjects;

namespace Elevate.FitnessCalculation.Domain.Entities
{
    public class CalculatedMetrics
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public MetabolicMetrics metabolic { get; set; }
        public Status Status { get; set; }
        public DateTime CalculatedAt { get; set; }
        public UserFitnessStats UserFitnessStats { get; private set; } = null!;
    }
}
