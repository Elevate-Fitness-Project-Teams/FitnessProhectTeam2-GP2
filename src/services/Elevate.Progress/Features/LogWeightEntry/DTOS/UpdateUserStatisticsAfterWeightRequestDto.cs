namespace Elevate.Progress.Features.LogWeightEntry.DTOS
{
    public class UpdateUserStatisticsAfterWeightRequestDto
    {
        public Guid UserId { get; set; }
        public decimal WeightDifference { get; set; }
    }
}
