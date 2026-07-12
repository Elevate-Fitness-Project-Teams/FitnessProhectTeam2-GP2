using Elevate.FitnessCalculation.Application.Features.DTOS;
using Elevate.FitnessCalculation.Domain.Exceptions;
using Elevate.Profile.Domain.Interfaces;
using MediatR;

namespace Elevate.FitnessCalculation.Application.Features.GetMetricCalc
{
    public class GetFitnessMetricsQueryHandler : IRequestHandler<GetFitnessMetricsQuery, FitnessMetricsDto>
    {
        private readonly IGeneralRepository<CalculatedMetrics> _repository;

        public GetFitnessMetricsQueryHandler(IGeneralRepository<CalculatedMetrics> repository)
        {
            _repository = repository;
          
        }
        public  async Task<FitnessMetricsDto> Handle(GetFitnessMetricsQuery request, CancellationToken cancellationToken)
        {
            var calculatedMetrics =await _repository.GetById(request.userId);
            if (calculatedMetrics == null) { throw new DomainValidException("FCE_METRICS_NOT_CALCULATED."); }

            return new FitnessMetricsDto
            {
                UserId = calculatedMetrics.UserId,
                bmr = calculatedMetrics.Metabolic.Bmr,
                tdee = calculatedMetrics.Metabolic.Tdee,
                calorieTarget = calculatedMetrics.Metabolic.CalorieTarget,
                status = calculatedMetrics.Status.ToString()
            };
            
        }
    }
}
