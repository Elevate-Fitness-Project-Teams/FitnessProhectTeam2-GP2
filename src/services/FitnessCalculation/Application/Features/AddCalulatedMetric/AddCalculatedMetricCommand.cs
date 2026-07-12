using MediatR;

namespace Elevate.FitnessCalculation.Application.Features.AddCalulatedMetric
{
    public record AddCalculatedMetricCommand(
            int userId,
            double bmr,
            double tdee,
            double calorieTarget
            ) : IRequest;
  
}
