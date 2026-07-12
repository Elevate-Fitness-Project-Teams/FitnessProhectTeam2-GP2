using Elevate.Progress.Features.ViewAchievements.DTOS;
using Elevate.Progress.Features.ViewAchievements.Query;
using Elevate.Progress.Shared.Exceptions;
using MediatR;

namespace Elevate.Progress.Features.ViewAchievements.Orchestrator
{
    public record ViewAchievementsOrchestrator(
        ViewAchievementsRequestDto RequestDto)
        : IRequest<ViewAchievementsResponseDto>;


    public class ViewAchievementsOrchestratorHandler
        : IRequestHandler<ViewAchievementsOrchestrator, ViewAchievementsResponseDto>
    {
        private readonly IMediator _mediator;

        public ViewAchievementsOrchestratorHandler(IMediator mediator)
        {
            _mediator = mediator;
        }


        public async Task<ViewAchievementsResponseDto> Handle(
            ViewAchievementsOrchestrator request,
            CancellationToken cancellationToken)
        {
            if (request.RequestDto.UserId == Guid.Empty)
                throw new InvalidUserIdException();


            var userAchievements = await _mediator.Send(
                new GetUserAchievementsQuery(
                    new GetUserAchievementsQueryRequest
                    {
                        UserId = request.RequestDto.UserId
                    }),
                cancellationToken);


            if (userAchievements.Count == 0)
            {
                return new ViewAchievementsResponseDto
                {
                    Achievements = []
                };
            }


            var achievementIds = userAchievements
                .Select(x => x.AchievementId)
                .ToList();


            var achievements = await _mediator.Send(
                new GetAchievementsQuery(
                    new GetAchievementsRequestDto
                    {
                        AchievementIds = achievementIds
                    }),
                cancellationToken);



            var response = userAchievements
                .Join(
                    achievements,
                    userAchievement => userAchievement.AchievementId,
                    achievement => achievement.Id,
                    (userAchievement, achievement) =>
                        new AchievementRecordDto
                        {
                            Name = achievement.Name,
                            Description = achievement.Description,
                            IconUrl = achievement.IconUrl,
                            EarnedAt = userAchievement.EarnedAt
                        })
                .ToList();


            return new ViewAchievementsResponseDto
            {
                Achievements = response
            };
        }
    }
}