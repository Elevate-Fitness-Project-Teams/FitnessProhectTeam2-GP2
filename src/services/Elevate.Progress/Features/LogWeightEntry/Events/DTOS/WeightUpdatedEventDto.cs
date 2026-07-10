namespace Elevate.Progress.Features.LogWeightEntry.Events.DTOS
{
    public class WeightUpdatedEventDto
    {
        public Guid UserId { get; set; }

        public decimal Weight { get; set; }

        public DateTime Date { get; set; }
    }
}
