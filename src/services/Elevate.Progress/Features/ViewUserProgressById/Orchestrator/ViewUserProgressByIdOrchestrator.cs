using Elevate.Progress.Features.ViewUserProgressById.DTOS;
using Elevate.Progress.Features.ViewUserProgressById.Query;
using Elevate.Progress.Shared.Exceptions;
using MediatR;

namespace Elevate.Progress.Features.ViewUserProgressById.Orchestrator
{
    public record ViewUserProgressByIdOrchestrator(
        ViewUserProgressByIdRequestDto RequestDto)
        : IRequest<ViewUserProgressByIdResponseDto>;

    public class ViewUserProgressByIdOrchestratorHandler
        : IRequestHandler<ViewUserProgressByIdOrchestrator, ViewUserProgressByIdResponseDto>
    {
        private readonly IMediator _mediator;

        public ViewUserProgressByIdOrchestratorHandler(IMediator mediator)
        {
            _mediator = mediator;
        }

        public async Task<ViewUserProgressByIdResponseDto> Handle(
            ViewUserProgressByIdOrchestrator request,
            CancellationToken cancellationToken)
        {
            // Validation
            if (request.RequestDto.UserId == Guid.Empty)
                throw new InvalidUserIdException();

            // Get User Statistics
            var statistics = await _mediator.Send(
                new GetUserStatisticsQuery(
                    new ViewUserStatisticsRequestDto
                    {
                        UserId = request.RequestDto.UserId
                    }),
                cancellationToken);

            if (statistics is null)
                throw new UserNotFoundException();

            // Get Latest Weight
            var latestWeight = await _mediator.Send(
                new GetLatestWeightQuery(
                    new GetLatestWeightRequestDto
                    {
                        UserId = request.RequestDto.UserId
                    }),
                cancellationToken);

            // Get Last Workout
            var lastWorkout = await _mediator.Send(
                new GetLastWorkoutQuery(
                    new GetLastWorkoutRequestDto
                    {
                        UserId = request.RequestDto.UserId
                    }),
                cancellationToken);

            // Build Response
            return new ViewUserProgressByIdResponseDto
            {
                TotalWorkouts = statistics.TotalWorkouts,
                TotalCaloriesBurned = statistics.TotalCaloriesBurned,
                TotalWeightLost = statistics.TotalWeightLost,
                CurrentStreak = statistics.CurrentStreak,
                LongestStreak = statistics.LongestStreak,
                CurrentWeight = latestWeight is null ? null : latestWeight.Weight,
                LastWorkoutDate = lastWorkout is null ? null : lastWorkout.LastWorkoutDate
            };
        }
    }
}