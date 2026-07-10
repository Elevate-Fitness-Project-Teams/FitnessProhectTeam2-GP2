using Elevate.Progress.Domain.Enums;
using Elevate.Progress.Features.Exception;
using Elevate.Progress.Features.LogWorkoutCompletion.Command;
using Elevate.Progress.Features.LogWorkoutCompletion.DTOS;
using Elevate.Progress.Features.LogWorkoutCompletion.Exception;
using Elevate.Progress.Features.LogWorkoutCompletion.Query;
using Elevate.Progress.Shared.Exceptions;
using MediatR;

namespace Elevate.Progress.Features.LogWorkoutCompletion.Orchestrator
{
    public record LogWorkoutCompletionOrchestrator(LogWorkoutCompletionRequestDto RequestDto)
        : IRequest<LogWorkoutCompletionResponseDto>;

    public class LogWorkoutCompletionOrchestratorHandler
        : IRequestHandler<LogWorkoutCompletionOrchestrator, LogWorkoutCompletionResponseDto>
    {
        private readonly IMediator _mediator;

        public LogWorkoutCompletionOrchestratorHandler(IMediator mediator)
        {
            _mediator = mediator;
        }

        public async Task<LogWorkoutCompletionResponseDto> Handle(
            LogWorkoutCompletionOrchestrator request,
            CancellationToken cancellationToken)
        {
            // 1. Get Workout Session
            var session = await _mediator.Send(
    new GetWorkoutSessionQuery(
        request.RequestDto.WorkoutSessionId,  request.RequestDto.UserId), cancellationToken);

            if (session == null)
                throw new NotFoundWorkoutSession(request.RequestDto.WorkoutSessionId);

            if (session.Status == WorkoutSessionStatus.Completed)
                throw new WorkoutSessionAlreadyCompletedException(request.RequestDto.WorkoutSessionId);
            // Validation
            if (request.RequestDto.Rating < 1 || request.RequestDto.Rating > 5)
                throw new BadRequestException(
                    "VAL_REQUIRED_FIELD",
                    "Rating must be between 1 and 5.");

            // 2. Create Workout Log
            var workoutLog = await _mediator.Send(
                new CreateWorkoutLogCommand(
                    new CreateWorkoutLogRequestDto
                    {
                        UserId = request.RequestDto.UserId,
                        WorkoutId = request.RequestDto.WorkoutId,
                        WorkoutSessionId = request.RequestDto.WorkoutSessionId,
                        CompletedAt = request.RequestDto.CompletedAt,
                        Duration = request.RequestDto.Duration,
                        CaloriesBurned = request.RequestDto.CaloriesBurned,
                        Difficulty = request.RequestDto.Difficulty,
                        Notes = request.RequestDto.Notes,
                        Rating = request.RequestDto.Rating
                    }),
                cancellationToken);

            // 3. Create Workout Log Exercises
            await _mediator.Send(
                new CreateWorkoutLogExercisesCommand(
                    workoutLog.WorkoutLogId,
                    request.RequestDto.Exercises),
                cancellationToken);

            // 4. Complete Workout Session
            await _mediator.Send(
                new CompleteWorkoutSessionCommand(
                    new CompleteWorkoutSessionRequestDto
                    {
                        WorkoutSessionId = request.RequestDto.WorkoutSessionId,
                        CompletedAt = request.RequestDto.CompletedAt
                    }),
                cancellationToken);

            // 5. Update Streak
            var streak = await _mediator.Send(
                new UpdateStreakCommand(
                    new UpdateStreakRequestDto
                    {
                        UserId = request.RequestDto.UserId,
                        CompletedAt = request.RequestDto.CompletedAt
                    }),
                cancellationToken);

            // 6. Update User Statistics
            await _mediator.Send(
                new UpdateUserStatisticsCommand(
                    new UpdateUserStatisticsRequestDto
                    {
                        UserId = request.RequestDto.UserId,
                        Duration = request.RequestDto.Duration,
                        CaloriesBurned = request.RequestDto.CaloriesBurned
                    }),
                cancellationToken);

            // 7. Evaluate Achievements
            var achievements = await _mediator.Send(
                new EvaluateUserAchievementsCommand(
                    new EvaluateUserAchievementsRequestDto
                    {
                        UserId = request.RequestDto.UserId
                    }),
                cancellationToken);

            return new LogWorkoutCompletionResponseDto
            {
                WorkoutLogId = workoutLog.WorkoutLogId,
                CurrentStreak = streak.CurrentStreak,
                Success = true
            };
        }
    }
}