namespace Elevate.FitnessCalculation.Application.Features.DTOS
{
    public class CalculatedMeticDTO
    {
        public int UserId { get; set; }
        public double bmr { get; set; }
        public double tdee { get; set; }
        public double calorieTarget { get; set; }

    }
}
