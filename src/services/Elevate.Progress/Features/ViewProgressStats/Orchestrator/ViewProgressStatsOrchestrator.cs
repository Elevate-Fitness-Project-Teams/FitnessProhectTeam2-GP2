using Elevate.Progress.Features.ViewProgressStats.DTOS;
using Elevate.Progress.Features.ViewProgressStats.Query;
using Elevate.Progress.Shared.Exceptions;
using MediatR;

namespace Elevate.Progress.Features.ViewProgressStats.Orchestrator
{
    public record ViewProgressStatsOrchestrator(
        ViewProgressStatsRequestDto RequestDto)
        : IRequest<ViewProgressStatsResponseDto>;

    public class ViewProgressStatsOrchestratorHandler
        : IRequestHandler<ViewProgressStatsOrchestrator, ViewProgressStatsResponseDto>
    {
        private readonly IMediator _mediator;

        public ViewProgressStatsOrchestratorHandler(IMediator mediator)
        {
            _mediator = mediator;
        }

        public async Task<ViewProgressStatsResponseDto> Handle(
            ViewProgressStatsOrchestrator request,
            CancellationToken cancellationToken)
        {
            if (request.RequestDto.UserId == Guid.Empty)
                throw new InvalidUserIdException();

            var statistics = await _mediator.Send(
                new GetUserStatisticsQuery(
                    new GetUserStatisticsRequestDto
                    {
                        UserId = request.RequestDto.UserId
                    }),
                cancellationToken);

            var streak = await _mediator.Send(
                new GetStreakQuery(
                    new GetStreakRequestDto
                    {
                        UserId = request.RequestDto.UserId
                    }),
                cancellationToken);

            return new ViewProgressStatsResponseDto
            {
                TotalWorkouts = statistics.TotalWorkouts,
                TotalCaloriesBurned = statistics.TotalCaloriesBurned,
                TotalWeightLost = statistics.TotalWeightLost,
                CurrentStreak = streak.CurrentStreak,
                LongestStreak = streak.LongestStreak
            };
        }
    }
}