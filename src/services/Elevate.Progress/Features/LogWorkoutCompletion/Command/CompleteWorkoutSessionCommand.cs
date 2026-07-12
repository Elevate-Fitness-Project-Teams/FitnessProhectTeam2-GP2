using Elevate.Progress.Domain.Entities;
using Elevate.Progress.Domain.Enums;
using Elevate.Progress.Features.Exception;
using Elevate.Progress.Features.LogWorkoutCompletion.DTOS;
using Elevate.Progress.Infrastructure.Persistence;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Elevate.Progress.Features.LogWorkoutCompletion.Command
{
    public record CompleteWorkoutSessionCommand
        (CompleteWorkoutSessionRequestDto WorkoutSessionRequestDto)
        : IRequest<CompleteWorkoutSessionResponseDto>;

    public class CompleteWorkoutSessionCommandHandler
        : IRequestHandler<CompleteWorkoutSessionCommand, CompleteWorkoutSessionResponseDto>
    {
        private readonly ProgressDbContext _context;

        public CompleteWorkoutSessionCommandHandler(ProgressDbContext context)
        {
            _context = context;
        }

        public async Task<CompleteWorkoutSessionResponseDto> Handle(
            CompleteWorkoutSessionCommand request,
            CancellationToken cancellationToken)
        {
            var session = await _context.WorkoutSessions
                .FirstOrDefaultAsync(
                    x => x.SessionId == request.WorkoutSessionRequestDto.WorkoutSessionId,
                    cancellationToken);

            if (session == null)
                throw new NotFoundWorkoutSession(request.WorkoutSessionRequestDto.WorkoutSessionId);

            session.Status = WorkoutSessionStatus.Completed;
            session.CompletedAt = request.WorkoutSessionRequestDto.CompletedAt;

            await _context.SaveChangesAsync(cancellationToken);

            return new CompleteWorkoutSessionResponseDto
            {
                Success = true
            };
        }
    }
}