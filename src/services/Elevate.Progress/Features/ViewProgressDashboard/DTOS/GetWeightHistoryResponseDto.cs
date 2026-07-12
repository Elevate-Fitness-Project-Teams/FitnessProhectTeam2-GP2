namespace Elevate.Progress.Features.ViewProgressDashboard.DTOS
{
    public class GetWeightHistoryResponseDto
    {
        public List<WeightHistoryRecordDto> WeightHistory { get; set; } = [];
    }

    public class WeightHistoryRecordDto
    {
        public decimal Weight { get; set; }

        public DateTime Date { get; set; }
    }
}
