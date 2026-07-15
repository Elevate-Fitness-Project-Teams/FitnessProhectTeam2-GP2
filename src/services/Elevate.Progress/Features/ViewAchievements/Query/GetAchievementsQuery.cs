using Elevate.Progress.Features.ViewAchievements.DTOS;
using Elevate.Progress.Infrastructure.Persistence.ProgressDbContext;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Elevate.Progress.Features.ViewAchievements.Query
{
    public record GetAchievementsQuery(
        GetAchievementsRequestDto RequestDto)
        : IRequest<List<AchievementDto>>;

    public class GetAchievementsQueryHandler
        : IRequestHandler<GetAchievementsQuery, List<AchievementDto>>
    {
        private readonly ProgressDbContext _context;

        public GetAchievementsQueryHandler(ProgressDbContext context)
        {
            _context = context;
        }

        public async Task<List<AchievementDto>> Handle(
            GetAchievementsQuery request,
            CancellationToken cancellationToken)
        {
            return await _context.Achievements
                .AsNoTracking()
                .Where(x => request.RequestDto.AchievementIds.Contains(x.Id))
                .Select(x => new AchievementDto
                {
                    Id = x.Id,
                    Name = x.Name,
                    Description = x.Description,
                    IconUrl = x.IconUrl
                })
                .ToListAsync(cancellationToken);
        }
    }
}