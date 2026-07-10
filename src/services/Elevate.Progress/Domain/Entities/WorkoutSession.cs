using Elevate.Progress.Domain.Enums;
using System.ComponentModel.DataAnnotations;

namespace Elevate.Progress.Domain.Entities
{
    public class WorkoutSession
    {

        [Key]
        public Guid SessionId { get; set; }

        public Guid UserId { get; set; }

        public Guid WorkoutId { get; set; }

        public WorkoutSessionStatus Status { get; set; }

        public DateTime StartedAt { get; set; }

        public DateTime? CompletedAt { get; set; } 
    }
}
