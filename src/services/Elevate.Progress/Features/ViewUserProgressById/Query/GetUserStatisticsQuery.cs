using Elevate.Progress.Features.ViewUserProgressById.DTOS;
using Elevate.Progress.Infrastructure.Persistence;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Elevate.Progress.Features.ViewUserProgressById.Query
{
    public record GetUserStatisticsQuery(
        ViewUserStatisticsRequestDto RequestDto)
        : IRequest<ViewUserStatisticsResponseDto?>;

    public class GetUserStatisticsQueryHandler
        : IRequestHandler<GetUserStatisticsQuery, ViewUserStatisticsResponseDto?>
    {
        private readonly ProgressDbContext _context;

        public GetUserStatisticsQueryHandler(ProgressDbContext context)
        {
            _context = context;
        }

        public async Task<ViewUserStatisticsResponseDto?> Handle(
            GetUserStatisticsQuery request,
            CancellationToken cancellationToken)
        {
            var statistics = await _context.UserStatistics
                .AsNoTracking()
                .FirstOrDefaultAsync(
                    x => x.UserId == request.RequestDto.UserId,
                    cancellationToken);

            if (statistics is null)
            {
                return null;
            }

            return new ViewUserStatisticsResponseDto
            {
                TotalWorkouts = statistics.TotalWorkouts,
                TotalCaloriesBurned = statistics.TotalCaloriesBurned,
                TotalWeightLost = statistics.TotalWeightLost,
                CurrentStreak = statistics.CurrentStreak,
                LongestStreak = statistics.LongestStreak
            };
        }
    }
}