using Elevate.FitnessCalculation.Domain.Enums;
using Elevate.FitnessCalculation.Domain.ValueObjects;

namespace Elevate.FitnessCalculation.Domain.Entities
{
    public class FitnessPlanConfig
    {
        public string PlanId { get; set; }
        public string PlanName { get; set; } = null!;
        public string? Description { get; set; }
        public Goal Goal { get; set; }
        public Status status { get; set; }
        public CaloriesRange caloriesRange { get; set; }
        public PlanConfigration planConfigration { get; set; }
        public ICollection<UserAssignedPlans> Plans { get; set; } = [];

        private FitnessPlanConfig() { }

        public FitnessPlanConfig(
        string planId,
        string planName,
        string description,
        Goal goal,
        Status status,
        CaloriesRange caloriesRange,
        PlanConfigration planConfiguration)
        {
            PlanId = planId;
            PlanName = planName;
            Description = description;
            Goal = goal;
            status = status;
            caloriesRange = caloriesRange;
            planConfiguration = planConfiguration;
        }


    }
}
