using Elevate.Progress.Features.ViewProgressStats.DTOS;
using Elevate.Progress.Infrastructure.Persistence.ProgressDbContext;
using Elevate.Progress.Shared.Exceptions;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Elevate.Progress.Features.ViewProgressStats.Query
{
    public record GetStreakQuery(
        GetStreakRequestDto RequestDto)
        : IRequest<GetStreakResponseDto>;

    public class GetStreakQueryHandler
        : IRequestHandler<GetStreakQuery, GetStreakResponseDto>
    {
        private readonly ProgressDbContext _context;

        public GetStreakQueryHandler(ProgressDbContext context)
        {
            _context = context;
        }

        public async Task<GetStreakResponseDto> Handle(
            GetStreakQuery request,
            CancellationToken cancellationToken)
        {
            if (request.RequestDto.UserId == Guid.Empty)
                throw new InvalidUserIdException();

            var streak = await _context.Streaks
                .AsNoTracking()
                .FirstOrDefaultAsync(
                    x => x.UserId == request.RequestDto.UserId,
                    cancellationToken);

            if (streak is null)
                throw new UserNotFoundException();

            return new GetStreakResponseDto
            {
                CurrentStreak = streak.CurrentStreak,
                LongestStreak = streak.LongestStreak
            };
        }
    }
}