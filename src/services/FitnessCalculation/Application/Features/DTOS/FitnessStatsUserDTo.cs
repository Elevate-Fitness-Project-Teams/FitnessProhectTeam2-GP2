namespace Elevate.FitnessCalculation.Application.Features.DTOS
{
    public class FitnessStatsUserDTo
    {
        public int UserId { get; set; }
        public DateTime RecordedAt { get; set; }
        public double Weight { get; set; }

        public double Height { get; set; }

        public int Age { get; set; }

    }
}
