using Elevate.Progress.Features.ViewUserProgressById.DTOS;
using Elevate.Progress.Infrastructure.Persistence;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Elevate.Progress.Features.ViewUserProgressById.Query
{
    public record GetLatestWeightQuery(GetLatestWeightRequestDto RequestDto)
        : IRequest<GetLatestWeightResponseDto?>;

    public class GetLatestWeightQueryHandler
        : IRequestHandler<GetLatestWeightQuery, GetLatestWeightResponseDto?>
    {
        private readonly ProgressDbContext _context;

        public GetLatestWeightQueryHandler(ProgressDbContext context)
        {
            _context = context;
        }

        public async Task<GetLatestWeightResponseDto?> Handle(
            GetLatestWeightQuery request,
            CancellationToken cancellationToken)
        {
            var latestWeight = await _context.WeightHistory
                .AsNoTracking()
                .Where(x => x.UserId == request.RequestDto.UserId)
                .OrderByDescending(x => x.Date)
                .FirstOrDefaultAsync(cancellationToken);

            if (latestWeight is null)
            {
                return null;
            }

            return new GetLatestWeightResponseDto
            {
                Weight = latestWeight.Weight,
                Date = latestWeight.Date
            };
        }
    }
}