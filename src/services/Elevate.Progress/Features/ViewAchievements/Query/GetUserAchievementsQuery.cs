using Elevate.Progress.Features.ViewAchievements.DTOS;
using Elevate.Progress.Infrastructure.Persistence.ProgressDbContext;
using Elevate.Progress.Shared.Exceptions;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Elevate.Progress.Features.ViewAchievements.Query
{
    public record GetUserAchievementsQuery(
        GetUserAchievementsQueryRequest RequestDto)
        : IRequest<List<UserAchievementDto>>;

    public class GetUserAchievementsQueryHandler
        : IRequestHandler<GetUserAchievementsQuery, List<UserAchievementDto>>
    {
        private readonly ProgressDbContext _context;

        public GetUserAchievementsQueryHandler(
            ProgressDbContext context)
        {
            _context = context;
        }

        public async Task<List<UserAchievementDto>> Handle(
            GetUserAchievementsQuery request,
            CancellationToken cancellationToken)
        {
            if (request.RequestDto.UserId == Guid.Empty)
                throw new InvalidUserIdException();

            return await _context.UserAchievements
                .AsNoTracking()
                .Where(x => x.UserId == request.RequestDto.UserId)
                .Select(x => new UserAchievementDto
                {
                    AchievementId = x.AchievementId,
                    EarnedAt = x.EarnedAt
                })
                .ToListAsync(cancellationToken);
        }
    }
}