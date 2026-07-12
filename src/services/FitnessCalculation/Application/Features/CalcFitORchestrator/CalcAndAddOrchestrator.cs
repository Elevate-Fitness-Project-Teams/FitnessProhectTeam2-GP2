using MediatR;

namespace Elevate.FitnessCalculation.Application.Features.CalcFitORchestrator
{
    public record CalcAndAddOrchestrator(int userId) : IRequest;
   
}
