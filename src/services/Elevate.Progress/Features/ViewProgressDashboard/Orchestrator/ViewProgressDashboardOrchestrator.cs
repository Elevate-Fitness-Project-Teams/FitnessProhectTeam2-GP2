using Elevate.Progress.Features.ViewProgressDashboard.DTOS;
using Elevate.Progress.Features.ViewProgressDashboard.Exception;
using Elevate.Progress.Features.ViewProgressDashboard.Query;
using Elevate.Progress.Shared.Exceptions;
using MediatR;

namespace Elevate.Progress.Features.ViewProgressDashboard.Orchestrator
{
    public record ViewProgressDashboardOrchestrator(
        ViewProgressDashboardRequestDto RequestDto)
        : IRequest<ViewProgressDashboardResponseDto>;

    public class ViewProgressDashboardOrchestratorHandler
        : IRequestHandler<ViewProgressDashboardOrchestrator, ViewProgressDashboardResponseDto>
    {
        private readonly IMediator _mediator;

        public ViewProgressDashboardOrchestratorHandler(IMediator mediator)
        {
            _mediator = mediator;
        }

        public async Task<ViewProgressDashboardResponseDto> Handle(
            ViewProgressDashboardOrchestrator request,
            CancellationToken cancellationToken)
        {
            if (request.RequestDto.UserId == Guid.Empty)
                throw new InvalidUserIdException();

            if (request.RequestDto.StartDate.HasValue &&
                request.RequestDto.EndDate.HasValue &&
                request.RequestDto.StartDate > request.RequestDto.EndDate)
            {
                throw new InvalidDateException();
            }

            var statistics = await _mediator.Send(
                new GetUserStatisticsQuery(
                    new GetUserStatisticsRequestDto
                    {
                        UserId = request.RequestDto.UserId,
                        Period = request.RequestDto.Period,
                        StartDate = request.RequestDto.StartDate,
                        EndDate = request.RequestDto.EndDate
                    }),
                cancellationToken);

            var weightHistory = await _mediator.Send(
                new GetWeightHistoryQuery(
                    new GetWeightHistoryRequestDto
                    {
                        UserId = request.RequestDto.UserId,
                        Period = request.RequestDto.Period,
                        StartDate = request.RequestDto.StartDate,
                        EndDate = request.RequestDto.EndDate
                    }),
                cancellationToken);

            var workoutHistory = await _mediator.Send(
                new GetWorkoutHistoryQuery(
                    new GetWorkoutHistoryRequestDto
                    {
                        UserId = request.RequestDto.UserId,
                        Period = request.RequestDto.Period,
                        StartDate = request.RequestDto.StartDate,
                        EndDate = request.RequestDto.EndDate
                    }),
                cancellationToken);

            var weekComparison = await _mediator.Send(
                new GetWeekComparisonQuery(
                    new GetWeekComparisonRequestDto
                    {
                        UserId = request.RequestDto.UserId
                    }),
                cancellationToken);

            var earnedAchievements = await _mediator.Send(
                new GetEarnedAchievementsQuery(
                    new GetEarnedAchievementsRequestDto
                    {
                        UserId = request.RequestDto.UserId
                    }),
                cancellationToken);

            if (weightHistory.WeightHistory.Count >= 2)
            {
                weekComparison.WeightDifference =
                    weightHistory.WeightHistory.First().Weight -
                    weightHistory.WeightHistory.Last().Weight;
            }

            return new ViewProgressDashboardResponseDto
            {
                SummaryStatistics = statistics,
                WeightHistory = weightHistory,
                WorkoutHistory = workoutHistory,
                WeekComparison = weekComparison,
                EarnedAchievements = earnedAchievements
            };
        }
    }
}