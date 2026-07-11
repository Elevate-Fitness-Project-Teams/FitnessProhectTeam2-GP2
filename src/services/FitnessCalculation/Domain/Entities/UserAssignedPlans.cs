namespace Elevate.FitnessCalculation.Domain.Entities
{
    public class UserAssignedPlans
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public string PlanId { get; set; } = null!;
        public bool sActive { get; set; }
        public DateTime AssignedAt { get; set; } = DateTime.UtcNow;
    }
}
