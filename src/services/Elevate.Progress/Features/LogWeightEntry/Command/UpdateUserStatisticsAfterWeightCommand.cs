using Elevate.Progress.Domain.Entities;
using Elevate.Progress.Features.LogWeightEntry.DTOS;
using Elevate.Progress.Infrastructure.Persistence.ProgressDbContext;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Elevate.Progress.Features.LogWeightEntry.Command
{
    public record UpdateUserStatisticsAfterWeightCommand(
        UpdateUserStatisticsAfterWeightRequestDto RequestDto)
        : IRequest<UpdateUserStatisticsAfterWeightResponseDto>;

    public class UpdateUserStatisticsAfterWeightCommandHandler
        : IRequestHandler<UpdateUserStatisticsAfterWeightCommand, UpdateUserStatisticsAfterWeightResponseDto>
    {
        private readonly ProgressDbContext _context;

        public UpdateUserStatisticsAfterWeightCommandHandler(ProgressDbContext context)
        {
            _context = context;
        }

        public async Task<UpdateUserStatisticsAfterWeightResponseDto> Handle(
            UpdateUserStatisticsAfterWeightCommand request,
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
                    TotalWeightLost = 0,
                    UpdatedAt = DateTime.UtcNow
                };

                _context.UserStatistics.Add(statistics);
            }

            statistics.UpdatedAt = DateTime.UtcNow;

            if (request.RequestDto.WeightDifference > 0)
            {
                statistics.TotalWeightLost += request.RequestDto.WeightDifference;
            }

            await _context.SaveChangesAsync(cancellationToken);

            return new UpdateUserStatisticsAfterWeightResponseDto
            {
                Success = true,
                TotalWeightLost = statistics.TotalWeightLost
            };
        }
    }
}