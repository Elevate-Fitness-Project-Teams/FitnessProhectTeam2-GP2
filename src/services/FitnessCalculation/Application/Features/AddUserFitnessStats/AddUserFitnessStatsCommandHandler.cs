using Elevate.FitnessCalculation.Domain.Entities;
using Elevate.FitnessCalculation.Domain.Enums;
using Elevate.FitnessCalculation.Domain.Exceptions;
using Elevate.FitnessCalculation.Domain.ValueObjects;
using Elevate.Profile.Domain.Interfaces;
using MediatR;

namespace Elevate.FitnessCalculation.Application.Features.AddUserFitnessStats
{
    public class AddUserFitnessStatsCommandHandler : IRequestHandler<AddUserFitnessStatsCommand>
    {
        private readonly IGeneralRepository<UserFitnessStats> _repository;
        private readonly IUnitOfWork _unitOfWork;

        public AddUserFitnessStatsCommandHandler(IGeneralRepository<UserFitnessStats> userFitnessStatsRepository,IUnitOfWork unitOfWork)
        {
            _repository = userFitnessStatsRepository;
            _unitOfWork = unitOfWork;
        }
        public async Task<Unit> Handle(AddUserFitnessStatsCommand request, CancellationToken cancellationToken)
        {
            if (!Enum.TryParse<Gender>(request.Gender, true, out var gender))
                throw new DomainValidException("VAL_INVALID_GENDER");

            if (!Enum.TryParse<Goal>(request.Goal, true, out var goal))
                throw new DomainValidException("VAL_INVALID_GOAL");

            if (!Enum.TryParse<ActivityLevel>(request.ActivityLevel, true, out var activityLevel))
                throw new DomainValidException("VAL_INVALID_ACTIVITY");


            var bodyMetrics = BodyMetrics.Create(
                                            request.Weight,
                                            request.Height,
                                            request.Age);

            var userFitnessStats = UserFitnessStats.Create(
                                                        request.UserId,
                                                        bodyMetrics,
                                                        gender,
                                                        goal,
                                                        activityLevel);
            await _unitOfWork.ExecuteAsync(async () =>
            {
                 _repository.Add(userFitnessStats);
            });

           return Unit.Value;
        }
    }
}
