using Elevate.FitnessCalculation.Application.Features.DTOS;
using Elevate.FitnessCalculation.Domain.Entities;
using Elevate.Profile.Domain.Interfaces;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Elevate.FitnessCalculation.Application.Features.GetAllConfigPlans
{
    public class GetAllConfigPlansQueryHandler : IRequestHandler<GetAllConfigPlansQuery, IEnumerable<PlansConfigDTO>>
    {
        private readonly IGeneralRepository<FitnessPlanConfig> _repository;                                     
        public GetAllConfigPlansQueryHandler(IGeneralRepository<FitnessPlanConfig> repository)
        {
            _repository = repository;
        }
        public async Task<IEnumerable<PlansConfigDTO>> Handle(GetAllConfigPlansQuery request, CancellationToken cancellationToken)
        {
            var plans = await _repository.GetAll().ToListAsync(cancellationToken);

            return plans.Select(plan=> new PlansConfigDTO
            {
                Goal=plan.Goal.ToString(),
                status=plan.status.ToString()
            });

        }
    }
}
