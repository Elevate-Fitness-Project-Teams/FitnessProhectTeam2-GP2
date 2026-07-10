namespace Elevate.Progress.Features.LogWeightEntry.DTOS
{
    public class LogWeightEntryRequestDto
    {
        public Guid UserId { get; set; }

        public decimal Weight { get; set; }

        public DateTime Date { get; set; }

        public string? Notes { get; set; }
    }
}
