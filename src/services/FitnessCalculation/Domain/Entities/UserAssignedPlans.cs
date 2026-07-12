namespace Elevate.FitnessCalculation.Domain.Entities
{
    public class UserAssignedPlans
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public string PlanId { get; set; } = null!;
        public bool IsActive { get; set; }
        public DateTime AssignedAt { get; set; } = DateTime.UtcNow;
        public FitnessPlanConfig plan { get; set; } = null!;
    }
}
