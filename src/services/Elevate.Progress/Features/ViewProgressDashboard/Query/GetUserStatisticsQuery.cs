using Elevate.Progress.Features.ViewProgressDashboard.DTOS;
using Elevate.Progress.Features.ViewProgressDashboard.Exception;
using Elevate.Progress.Infrastructure.Persistence;
using Elevate.Progress.Shared.Exceptions;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Elevate.Progress.Features.ViewProgressDashboard.Query
{
    public record GetUserStatisticsQuery(
        GetUserStatisticsRequestDto RequestDto)
        : IRequest<GetUserStatisticsResponseDto>;

    public class GetUserStatisticsQueryHandler
        : IRequestHandler<GetUserStatisticsQuery, GetUserStatisticsResponseDto>
    {
        private readonly ProgressDbContext _context;

        public GetUserStatisticsQueryHandler(ProgressDbContext context)
        {
            _context = context;
        }

        public async Task<GetUserStatisticsResponseDto> Handle(
            GetUserStatisticsQuery request,
            CancellationToken cancellationToken)
        {
            if (request.RequestDto.UserId == Guid.Empty)
                throw new InvalidUserIdException();

            if (request.RequestDto.StartDate.HasValue &&
                request.RequestDto.EndDate.HasValue &&
                request.RequestDto.StartDate > request.RequestDto.EndDate)
            {
                throw new InvalidDateException();
            }

            var query = _context.WorkoutLogs
                .AsNoTracking()
                .Where(x => x.UserId == request.RequestDto.UserId);

            if (request.RequestDto.StartDate.HasValue)
            {
                query = query.Where(x =>
                    x.CompletedAt >= request.RequestDto.StartDate.Value);
            }

            if (request.RequestDto.EndDate.HasValue)
            {
                query = query.Where(x =>
                    x.CompletedAt <= request.RequestDto.EndDate.Value);
            }

            var workouts = await query.ToListAsync(cancellationToken);

            decimal totalWeightLost = 0;

            var weightQuery = _context.WeightHistory
                .AsNoTracking()
                .Where(x => x.UserId == request.RequestDto.UserId);

            if (request.RequestDto.StartDate.HasValue)
            {
                weightQuery = weightQuery.Where(x =>
                    x.Date >= request.RequestDto.StartDate.Value);
            }

            if (request.RequestDto.EndDate.HasValue)
            {
                weightQuery = weightQuery.Where(x =>
                    x.Date <= request.RequestDto.EndDate.Value);
            }

            var weights = await weightQuery
                .OrderBy(x => x.Date)
                .ToListAsync(cancellationToken);

            if (weights.Count >= 2)
            {
                totalWeightLost =
                    weights.First().Weight - weights.Last().Weight;
            }

            return new GetUserStatisticsResponseDto
            {
                TotalWorkouts = workouts.Count,
                TotalCaloriesBurned = workouts.Sum(x => x.CaloriesBurned),
                TotalWeightLost = totalWeightLost
            };
        }
    }
}