namespace Elevate.Progress.Integration.Clients
{
    public sealed class FitnessStatsResponse
    {
        public decimal Height { get; set; }

        public decimal Weight { get; set; }

        public int Age { get; set; }

        public string Gender { get; set; } = string.Empty;

        public string Goal { get; set; } = string.Empty;

        public string ActivityLevel { get; set; } = string.Empty;
    }
}
