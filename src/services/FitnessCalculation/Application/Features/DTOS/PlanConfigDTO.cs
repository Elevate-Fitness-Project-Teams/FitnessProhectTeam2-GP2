using Elevate.FitnessCalculation.Domain.ValueObjects;

namespace Elevate.FitnessCalculation.Application.Features.DTOS
{
    public class PlanConfigDTO
    {
        public string duration { get; set; }
        public double MinCalorie { get; set; }
        public double MaxCalorie { get; set; }

        public string ProgramType { get; set; } = null!;
        public int workoutsPerWeek { get; set; }

    }
}
