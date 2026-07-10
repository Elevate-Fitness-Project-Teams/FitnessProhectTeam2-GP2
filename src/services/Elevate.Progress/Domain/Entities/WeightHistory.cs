namespace Elevate.Progress.Domain.Entities
{
    public class WeightHistory
    {
        public Guid Id { get; set; }

        public Guid UserId { get; set; }

        public decimal Weight { get; set; }

        public DateTime Date { get; set; }

        public string? Notes { get; set; }
    }
}
