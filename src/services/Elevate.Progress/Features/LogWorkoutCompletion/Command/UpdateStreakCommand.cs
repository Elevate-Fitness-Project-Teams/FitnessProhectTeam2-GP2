using Elevate.Progress.Domain.Entities;
using Elevate.Progress.Features.LogWorkoutCompletion.DTOS;
using Elevate.Progress.Infrastructure.Persistence.ProgressDbContext;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Elevate.Progress.Features.LogWorkoutCompletion.Command
{
    public record UpdateStreakCommand(UpdateStreakRequestDto Request)
        : IRequest<UpdateStreakResponseDto>;

    public class UpdateStreakCommandHandler
        : IRequestHandler<UpdateStreakCommand, UpdateStreakResponseDto>
    {
        private readonly ProgressDbContext _context;

        public UpdateStreakCommandHandler(ProgressDbContext context)
        {
            _context = context;
        }

        public async Task<UpdateStreakResponseDto> Handle(
            UpdateStreakCommand request,
            CancellationToken cancellationToken)
        {
            var streak = await _context.Streaks
                .FirstOrDefaultAsync(
                    x => x.UserId == request.Request.UserId,
                    cancellationToken);

            if (streak == null)
            {
                streak = new Streak
                {
                    UserId = request.Request.UserId,
                    CurrentStreak = 1,
                    LastWorkoutDate = request.Request.CompletedAt.Date
                };

                _context.Streaks.Add(streak);
            }
            else
            {
                var lastDate = streak.LastWorkoutDate.Date;
                var today = request.Request.CompletedAt.Date;

                if (lastDate == today)
                {
                   
                }
                else if (lastDate == today.AddDays(-1))
                {
                    streak.CurrentStreak++;
                }
                else
                {
                    streak.CurrentStreak = 1;
                }

                streak.LastWorkoutDate = today;
            }

            await _context.SaveChangesAsync(cancellationToken);

            return new UpdateStreakResponseDto
            {
                CurrentStreak = streak.CurrentStreak
            };
        }
    }
}