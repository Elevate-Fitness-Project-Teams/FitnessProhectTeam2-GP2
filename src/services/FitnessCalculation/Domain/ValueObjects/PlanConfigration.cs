namespace Elevate.FitnessCalculation.Domain.ValueObjects
{
    public sealed class PlanConfigration
    {
        public string EstimatedDuration { get; }
        public int WorkOutsperWeek { get;  }
        public string ProgramType { get;  }

        private PlanConfigration(string estimatedDuration, int workOutsperWeek, string programType)
        {
            if (string.IsNullOrWhiteSpace(estimatedDuration))
                throw new ArgumentException("Estimated duration cannot be null or empty.", nameof(estimatedDuration));
            if (workOutsperWeek <= 0)
                throw new ArgumentException("Workouts per week must be positive.", nameof(workOutsperWeek));
            if (string.IsNullOrWhiteSpace(programType))
                throw new ArgumentException("Program type cannot be null or empty.", nameof(programType));

            EstimatedDuration = estimatedDuration;
            WorkOutsperWeek = workOutsperWeek;
            ProgramType = programType;
        }

        public static PlanConfigration Create(string estimatedDuration, int workOutsperWeek, string programType)
        {
            return new PlanConfigration(estimatedDuration, workOutsperWeek, programType);
        }
    }
}
