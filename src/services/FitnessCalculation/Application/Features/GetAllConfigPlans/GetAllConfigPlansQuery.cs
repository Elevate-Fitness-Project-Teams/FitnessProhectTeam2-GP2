using Elevate.FitnessCalculation.Application.Features.DTOS;
using MediatR;

namespace Elevate.FitnessCalculation.Application.Features.GetAllConfigPlans
{
    public record GetAllConfigPlansQuery : IRequest<IEnumerable<PlansConfigDTO>>;

}
