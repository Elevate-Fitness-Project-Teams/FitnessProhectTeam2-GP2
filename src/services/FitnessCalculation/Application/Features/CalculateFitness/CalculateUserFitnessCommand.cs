using Elevate.FitnessCalculation.Application.Features.DTOS;
using MediatR;

namespace Elevate.FitnessCalculation.Application.Features.CalculateFitness

{
    public record CalculateUserFitnessCommand(int userId) : IRequest<CalculatedMeticDTO>;
   
}
