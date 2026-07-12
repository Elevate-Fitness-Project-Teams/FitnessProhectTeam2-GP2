using System.ComponentModel.DataAnnotations;

namespace Elevate.FitnessCalculation.Domain.Entities
{
    public class UserPlanHistory
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public string PlanId { get; set; } = null!;
        public DateTime AssignedAt { get; set; }
        public DateTime? EndedAt { get; set; }
        [MaxLength(225)]
        public string? ReasonForChange { get; set; }
    }
}
