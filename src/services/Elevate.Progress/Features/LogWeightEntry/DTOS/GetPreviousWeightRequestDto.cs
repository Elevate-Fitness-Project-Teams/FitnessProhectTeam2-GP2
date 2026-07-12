namespace Elevate.Progress.Features.LogWeightEntry.DTOS
{
    public class GetPreviousWeightRequestDto
    {
        public Guid UserId { get; set; }

        public DateTime Date { get; set; }
    }
}
