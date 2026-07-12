using Elevate.Progress.Domain.Entities;
using Elevate.Progress.Features.LogWorkoutCompletion.DTOS;
using Elevate.Progress.Infrastructure.Persistence;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Elevate.Progress.Features.LogWorkoutCompletion.Command
{
    public record UpdateUserStatisticsCommand(UpdateUserStatisticsRequestDto RequestDto)
        : IRequest<UpdateUserStatisticsResponseDto>;

    public class UpdateUserStatisticsCommandHandler
        : IRequestHandler<UpdateUserStatisticsCommand, UpdateUserStatisticsResponseDto>
    {
        private readonly ProgressDbContext _context;

        public UpdateUserStatisticsCommandHandler(ProgressDbContext context)
        {
            _context = context;
        }

        public async Task<UpdateUserStatisticsResponseDto> Handle(
            UpdateUserStatisticsCommand request,
            CancellationToken cancellationToken)
        {
            var statistics = await _context.UserStatistics
                .FirstOrDefaultAsync(
                    x => x.UserId == request.RequestDto.UserId,
                    cancellationToken);

            if (statistics == null)
            {
                statistics = new UserStatistics
                {
                    UserId = request.RequestDto.UserId,
                    TotalWorkouts = 1,
                    TotalWorkoutDuration = request.RequestDto.Duration,
                    TotalCaloriesBurned = request.RequestDto.CaloriesBurned,
                    UpdatedAt = DateTime.UtcNow
                };

                _context.UserStatistics.Add(statistics);
            }
            else
            {
                statistics.TotalWorkouts++;
                statistics.TotalWorkoutDuration += request.RequestDto.Duration;
                statistics.TotalCaloriesBurned += request.RequestDto.CaloriesBurned;
                statistics.UpdatedAt = DateTime.UtcNow;
            }

            await _context.SaveChangesAsync(cancellationToken);

            return new UpdateUserStatisticsResponseDto
            {
                Success = true
            };
        }
    }
}