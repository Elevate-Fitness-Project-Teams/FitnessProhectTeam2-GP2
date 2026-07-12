using Elevate.Progress.Features.LogWeightEntry.DTOS;
using Elevate.Progress.Infrastructure.Persistence.ProgressDbContext;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Elevate.Progress.Features.LogWeightEntry.Query
{
    public record GetPreviousWeightQuery(GetPreviousWeightRequestDto RequestDto)
        : IRequest<PreviousWeightResponseDto>;

    public class GetPreviousWeightQueryHandler
        : IRequestHandler<GetPreviousWeightQuery, PreviousWeightResponseDto>
    {
        private readonly ProgressDbContext _context;

        public GetPreviousWeightQueryHandler(ProgressDbContext context)
        {
            _context = context;
        }

        public async Task<PreviousWeightResponseDto> Handle(
            GetPreviousWeightQuery request,
            CancellationToken cancellationToken)
        {
            var previousWeightEntry = await _context.WeightHistory
                .AsNoTracking()
                .Where(x =>
                    x.UserId == request.RequestDto.UserId &&
                    x.Date < request.RequestDto.Date)
                .OrderByDescending(x => x.Date)
                .FirstOrDefaultAsync(cancellationToken);

            return new PreviousWeightResponseDto
            {
                PreviousWeight = previousWeightEntry?.Weight,
               
            };
        }
    }
}