using Elevate.FitnessCalculation.Application.Features.DTOS;
using MediatR;

namespace Elevate.FitnessCalculation.Application.Features.GetMetricCalc
{
    public record GetFitnessMetricsQuery(int userId): IRequest<FitnessMetricsDto>
    {

    }
}
