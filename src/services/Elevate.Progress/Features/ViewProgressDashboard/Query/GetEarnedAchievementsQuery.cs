using Elevate.Progress.Features.ViewProgressDashboard.DTOS;
using Elevate.Progress.Infrastructure.Persistence.ProgressDbContext;
using Elevate.Progress.Shared.Exceptions;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Elevate.Progress.Features.ViewProgressDashboard.Query
{
    public record GetEarnedAchievementsQuery(
        GetEarnedAchievementsRequestDto RequestDto)
        : IRequest<GetEarnedAchievementsResponseDto>;

    public class GetEarnedAchievementsQueryHandler
        : IRequestHandler<GetEarnedAchievementsQuery, GetEarnedAchievementsResponseDto>
    {
        private readonly ProgressDbContext _context;

        public GetEarnedAchievementsQueryHandler(ProgressDbContext context)
        {
            _context = context;
        }

        public async Task<GetEarnedAchievementsResponseDto> Handle(
            GetEarnedAchievementsQuery request,
            CancellationToken cancellationToken)
        {
            if (request.RequestDto.UserId == Guid.Empty)
                throw new InvalidUserIdException();

            var achievements = await (
                from ua in _context.UserAchievements.AsNoTracking()
                join a in _context.Achievements.AsNoTracking()
                    on ua.AchievementId equals a.Id
                where ua.UserId == request.RequestDto.UserId
                orderby ua.EarnedAt descending
                select new EarnedAchievementRecordDto
                {
                    Name = a.Name,
                    Description = a.Description,
                    IconUrl = a.IconUrl,
                    EarnedAt = ua.EarnedAt
                })
                .ToListAsync(cancellationToken);

            return new GetEarnedAchievementsResponseDto
            {
                Achievements = achievements
            };
        }
    }
}