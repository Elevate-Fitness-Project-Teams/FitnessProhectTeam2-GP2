using Elevate.FitnessCalculation.Domain.Entities;
using Elevate.FitnessCalculation.Domain.Enums;
using Elevate.FitnessCalculation.Domain.ValueObjects;
using Elevate.Profile.Domain.Interfaces;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Elevate.FitnessCalculation.Application.Features.AddCalulatedMetric
{
    public class AddCalculatedMetricCommandHandler : IRequestHandler<AddCalculatedMetricCommand>
    {
        private readonly IGeneralRepository<CalculatedMetrics> _repository;
        private readonly IUnitOfWork _unitOfWork;

        public AddCalculatedMetricCommandHandler(IGeneralRepository<CalculatedMetrics> repository,IUnitOfWork unitOfWork)
        {
           _repository = repository;
            _unitOfWork = unitOfWork;
        }
        public async Task<Unit> Handle(AddCalculatedMetricCommand request, CancellationToken cancellationToken)
        {
            Status status = request.calorieTarget switch
            {
                <= 1800 => Status.Weak,
                <= 2500 => Status.Normal,
                _ => Status.Hard
            };

            var metrics = await _repository.GetAll()
                .FirstOrDefaultAsync(x => x.UserId == request.userId, cancellationToken);

            await _unitOfWork.ExecuteAsync(async () =>
            {
                if (metrics == null)
                {
                    metrics = CalculatedMetrics.Create(
                        request.userId,
                        MetabolicMetrics.Create(
                            request.bmr,
                            request.tdee,
                            request.calorieTarget),
                        status);
                     _repository.Add(metrics);
                }
                else
                {
                    metrics.Update(
                        MetabolicMetrics.Create(
                            request.bmr,
                            request.tdee,
                            request.calorieTarget),
                        status);
                    _repository.Update(metrics);
                }
            });

            return Unit.Value;
        }
    }
}
