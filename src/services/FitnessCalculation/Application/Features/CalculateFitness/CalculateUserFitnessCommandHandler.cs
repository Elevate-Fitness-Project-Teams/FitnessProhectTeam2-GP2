using Elevate.FitnessCalculation.Application.Features.DTOS;
using Elevate.FitnessCalculation.Domain.Entities;
using Elevate.FitnessCalculation.Domain.Enums;
using Elevate.FitnessCalculation.Domain.Exceptions;
using Elevate.Profile.Domain.Interfaces;
using MediatR;

namespace Elevate.FitnessCalculation.Application.Features.CalculateFitness
{
    public class CalculateUserFitnessCommandHandler : IRequestHandler<CalculateUserFitnessCommand, CalculatedMeticDTO>
    {
        private readonly IGeneralRepository<UserFitnessStats> _repository;
        private double _bmr;
        private double _TDEE;
        private double _CalorieTarget;

        public CalculateUserFitnessCommandHandler(IGeneralRepository<UserFitnessStats> repository)
        {
            _repository = repository;
        }
        public async Task<CalculatedMeticDTO> Handle(CalculateUserFitnessCommand request, CancellationToken cancellationToken)
        {
            var userFitnessStats =await _repository.GetById(request.userId);
            if (userFitnessStats == null)
            {
                throw new NotFoundException($"FCE_STATS_NOT_FOUND");
            }

            //check male or female

            if(userFitnessStats.Gender==Gender.Male)
            {
                // Perform male-specific calculations
                 _bmr=userFitnessStats.BodyMetrics.Weight * 10 + 6.25 * userFitnessStats.BodyMetrics.Height - 5 * userFitnessStats.BodyMetrics.Age + 5;

            }
            else
            {
                _bmr=userFitnessStats.BodyMetrics.Weight * 10 + 6.25 * userFitnessStats.BodyMetrics.Height - 5 * userFitnessStats.BodyMetrics.Age - 161;
            }

            var activityFactor = userFitnessStats.ActivityLevel switch
            {
                ActivityLevel.Rookie => 1.2,
                ActivityLevel.Beginner => 1.375,
                ActivityLevel.Intermediate => 1.55,
                ActivityLevel.Advanced => 1.725,
                ActivityLevel.TrueBeast => 1.9,
                _ => throw new DomainValidException("VAL_INVALID_ACTIVITY")
            };

            _TDEE = _bmr * activityFactor;
            _CalorieTarget = userFitnessStats.Goal switch
            {
                Goal.LoseWeight => _TDEE - 500,
                Goal.GainWeight => _TDEE + 300,
                Goal.GainMoreFlexibility => _TDEE + 150,
                Goal.GetFitter => _TDEE,
                Goal.LearnTheBasic => _TDEE,
                _ => throw new DomainValidException("VAL_INVALID_GOAL")
            };

            if (double.IsNaN(_bmr) ||
                        double.IsInfinity(_bmr) ||
                        double.IsNaN(_TDEE) ||
                        double.IsInfinity(_TDEE) ||
                        double.IsNaN(_CalorieTarget) ||
                        double.IsInfinity(_CalorieTarget))
            {
                throw new DomainValidException("FCE_INVALID_CALCULATION");
            }

            return new CalculatedMeticDTO
            {
                bmr=_bmr,
                tdee=_TDEE,
                calorieTarget=_CalorieTarget,
                UserId=request.userId
            };
          
        }
    }
}
