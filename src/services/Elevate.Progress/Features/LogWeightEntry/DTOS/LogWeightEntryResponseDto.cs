namespace Elevate.Progress.Features.LogWeightEntry.DTOS
{
    public class LogWeightEntryResponseDto
    {
        public decimal? Bmi { get; set; }

        public decimal? DifferenceFromPrevious { get; set; }

        public decimal TotalWeightLost { get; set; }

        public bool Success { get; set; }
    }
}
