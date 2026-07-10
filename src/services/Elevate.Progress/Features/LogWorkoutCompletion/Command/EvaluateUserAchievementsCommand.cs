using Elevate.Progress.Features.LogWorkoutCompletion.DTOS;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace Elevate.Progress.Features.LogWorkoutCompletion.Command
{
    public record EvaluateUserAchievementsCommand(EvaluateUserAchievementsRequestDto RequestDto)
        : IRequest<EvaluateUserAchievementsResponseDto>;

    public class EvaluateUserAchievementsCommandHandler
        : IRequestHandler<EvaluateUserAchievementsCommand, EvaluateUserAchievementsResponseDto>
    {
        public async Task<EvaluateUserAchievementsResponseDto> Handle(
            EvaluateUserAchievementsCommand request,
            CancellationToken cancellationToken)
        {
            // TODO: future logic (calculate achievements)

            var newAchievements = new List<Guid>(); // هيتبني بعدين

            var result = new EvaluateUserAchievementsResponseDto
            {
                Success = true,
                NewAchievementsCount = newAchievements.Count,
                NewAchievementIds = newAchievements
            };

            // Trigger notification later
            if (newAchievements.Any())
            {
                // TODO: Publish event -> Notification Service
                // e.g. AchievementEarnedEvent
            }

            return result;
        }
    }
}