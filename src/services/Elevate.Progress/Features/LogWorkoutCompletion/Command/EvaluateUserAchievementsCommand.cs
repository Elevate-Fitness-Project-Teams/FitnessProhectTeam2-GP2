using Elevate.Progress.Features.LogWorkoutCompletion.DTOS;
using MediatR;

namespace Elevate.Progress.Features.LogWorkoutCompletion.Command
{
    public record EvaluateUserAchievementsCommand(
        EvaluateUserAchievementsRequestDto RequestDto)
        : IRequest<EvaluateUserAchievementsResponseDto>;


    public class EvaluateUserAchievementsCommandHandler
        : IRequestHandler<EvaluateUserAchievementsCommand, EvaluateUserAchievementsResponseDto>
    {
        public async Task<EvaluateUserAchievementsResponseDto> Handle(
            EvaluateUserAchievementsCommand request,
            CancellationToken cancellationToken)
        {
        

            var response = new EvaluateUserAchievementsResponseDto
            {
                Success = true,
                NewAchievementsCount = 0,
                NewAchievementIds = new List<Guid>()
            };

            return await Task.FromResult(response);
        }
    }
}