using Elevate.Progress.Features.ViewWeightHistory.DTOS;
using Elevate.Progress.Infrastructure.Persistence.ProgressDbContext;
using Elevate.Progress.Shared.Exceptions;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Elevate.Progress.Features.ViewWeightHistory.Query
{
    public record GetWeightHistoryQuery(
        WeightHistoryRequestDto RequestDto)
        : IRequest<WeightHistoryResponseDto>;

    public class GetWeightHistoryQueryHandler
        : IRequestHandler<GetWeightHistoryQuery, WeightHistoryResponseDto>
    {
        private readonly ProgressDbContext _context;

        public GetWeightHistoryQueryHandler(ProgressDbContext context)
        {
            _context = context;
        }

        public async Task<WeightHistoryResponseDto> Handle(
            GetWeightHistoryQuery request,
            CancellationToken cancellationToken)
        {
            if (request.RequestDto.UserId == Guid.Empty)
                throw new InvalidUserIdException();

            var weightHistory = await _context.WeightHistory
                .AsNoTracking()
                .Where(x => x.UserId == request.RequestDto.UserId)
                .OrderBy(x => x.Date)
                .Select(x => new WeightHistoryRecord
                {
                    Date = x.Date,
                    Weight = x.Weight
                })
                .ToListAsync(cancellationToken);

            return new WeightHistoryResponseDto
            {
                WeightRecords = weightHistory
            };
        }
    }
}