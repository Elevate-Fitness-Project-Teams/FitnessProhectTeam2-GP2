using Elevate.FitnessCalculation.Application.Features.DTOS;
using Elevate.FitnessCalculation.Domain.Entities;
using Elevate.FitnessCalculation.Domain.Exceptions;
using Elevate.Profile.Domain.Interfaces;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Elevate.FitnessCalculation.Application.Features.GetPlanConfig
{
    public class GetPlanConfigQueryHandler : IRequestHandler<GetPlanConfigQuery, PlanConfigDTO>
    {
        private readonly IGeneralRepository<FitnessPlanConfig> _repository;

        public GetPlanConfigQueryHandler(IGeneralRepository<FitnessPlanConfig> repository)
        {
            _repository = repository;
        }
        public async Task<PlanConfigDTO> Handle(GetPlanConfigQuery request, CancellationToken cancellationToken)
        {
            var plan =await _repository.GetAll()
                            .Where(x=>x.PlanId==request.planId).FirstOrDefaultAsync(cancellationToken);
            if (plan == null) { throw new NotFoundException("RES_PLAN_NOT_FOUND."); }
            return new PlanConfigDTO()
            {
                duration=plan.planConfigration.EstimatedDuration,
                MinCalorie=plan.caloriesRange.MinCalorie,
                MaxCalorie=plan.caloriesRange.MaxCalorie,
                ProgramType=plan.planConfigration.ProgramType,
                workoutsPerWeek=plan.planConfigration.WorkOutsperWeek
            };
        }
    }
}
