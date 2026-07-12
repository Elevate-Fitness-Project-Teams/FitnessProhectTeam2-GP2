using Elevate.Progress.Features.ViewProgressStats.DTOS;
using Elevate.Progress.Infrastructure.Persistence.ProgressDbContext;
using Elevate.Progress.Shared.Exceptions;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Elevate.Progress.Features.ViewProgressStats.Query
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

            var statistics = await _context.UserStatistics
                .AsNoTracking()
                .FirstOrDefaultAsync(
                    x => x.UserId == request.RequestDto.UserId,
                    cancellationToken);

            if (statistics is null)
                throw new UserNotFoundException();

            return new GetUserStatisticsResponseDto
            {
                TotalWorkouts = statistics.TotalWorkouts,
                TotalCaloriesBurned = statistics.TotalCaloriesBurned,
                TotalWeightLost = statistics.TotalWeightLost
            };
        }
    }
}