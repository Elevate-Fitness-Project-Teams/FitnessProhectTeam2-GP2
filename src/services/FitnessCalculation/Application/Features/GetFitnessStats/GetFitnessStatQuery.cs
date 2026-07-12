using Elevate.FitnessCalculation.Application.Features.DTOS;
using MediatR;

namespace Elevate.FitnessCalculation.Application.Features.GetFitnessStats
{
    public record GetFitnessStatQuery(int userId): IRequest<FitnessStatsUserDTo>;
   
}
