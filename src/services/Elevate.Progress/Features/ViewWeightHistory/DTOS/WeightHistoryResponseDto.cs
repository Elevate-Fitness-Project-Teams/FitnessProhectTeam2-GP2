namespace Elevate.Progress.Features.ViewWeightHistory.DTOS
{
    public class WeightHistoryResponseDto
    {
        public List<WeightHistoryRecord> WeightRecords { get; set; } = new List<WeightHistoryRecord>();
    }
    public class WeightHistoryRecord
    {
        public DateTime Date { get; set; }
        public decimal Weight { get; set; }
    }
}
