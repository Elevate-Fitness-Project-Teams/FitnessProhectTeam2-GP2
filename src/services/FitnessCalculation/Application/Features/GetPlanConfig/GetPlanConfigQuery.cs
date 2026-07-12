using Elevate.FitnessCalculation.Application.Features.DTOS;
using MediatR;

namespace Elevate.FitnessCalculation.Application.Features.GetPlanConfig
{
    public record GetPlanConfigQuery(string planId): IRequest<PlanConfigDTO>;
  
}
