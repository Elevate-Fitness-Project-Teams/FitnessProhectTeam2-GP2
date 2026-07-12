namespace Elevate.FitnessCalculation.Application.Features.DTOS
{
    public class AddCalulatedMericsDTO
    {
        public int UserId { get; set; }
        public double Bmr { get; set; }
        public double Tdee { get; set; }
        public double CalorieTarget { get; set; }
        public string Status { get; set; } = default!;
        public DateTime CalculatedAt { get; set; }
    }
}
