using Elevate.FitnessCalculation.Application.Features.DTOS;
using Elevate.FitnessCalculation.Domain.Entities;
using Elevate.FitnessCalculation.Domain.Exceptions;
using Elevate.Profile.Domain.Interfaces;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Elevate.FitnessCalculation.Application.Features.GetFitnessStats
{
    public class GetFitnessStatsQueryHandler : IRequestHandler<GetFitnessStatQuery, FitnessStatsUserDTo>
    {
        private readonly IGeneralRepository<UserFitnessStats> _repository;

        public GetFitnessStatsQueryHandler(IGeneralRepository<UserFitnessStats> repository)
        {
            _repository = repository;
        }
        public async Task<FitnessStatsUserDTo> Handle(GetFitnessStatQuery request, CancellationToken cancellationToken)
        {
            var fitnessStats =await _repository.GetAll()
                                .Where(x => x.UserId == request.userId)
                                .FirstOrDefaultAsync(cancellationToken);
            if (fitnessStats == null) { throw new NotFoundException("FCE_STATS_NOT_FOUND."); }

            return new FitnessStatsUserDTo()
            {
                UserId= fitnessStats.UserId,
                RecordedAt= fitnessStats.RecordedAt,
                Weight= fitnessStats.BodyMetrics.Weight,
                Height= fitnessStats.BodyMetrics.Height,
                Age= fitnessStats.BodyMetrics.Age
            };
        }
    }
}
