using Elevate.Progress.Domain.Enums;
using Elevate.Progress.Features.ViewProgressDashboard.DTOS;
using Elevate.Progress.Features.ViewProgressDashboard.Exception;
using Elevate.Progress.Infrastructure.Persistence.ProgressDbContext;
using Elevate.Progress.Shared.Exceptions;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Elevate.Progress.Features.ViewProgressDashboard.Query
{
    public record GetWeightHistoryQuery(
        GetWeightHistoryRequestDto RequestDto)
        : IRequest<GetWeightHistoryResponseDto>;

    public class GetWeightHistoryQueryHandler
        : IRequestHandler<GetWeightHistoryQuery, GetWeightHistoryResponseDto>
    {
        private readonly ProgressDbContext _context;

        public GetWeightHistoryQueryHandler(ProgressDbContext context)
        {
            _context = context;
        }

        public async Task<GetWeightHistoryResponseDto> Handle(
            GetWeightHistoryQuery request,
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

            var query = _context.WeightHistory
                .AsNoTracking()
                .Where(x => x.UserId == request.RequestDto.UserId);

            // Priority: explicit date range
            if (request.RequestDto.StartDate.HasValue || request.RequestDto.EndDate.HasValue)
            {
                if (request.RequestDto.StartDate.HasValue)
                {
                    query = query.Where(x => x.Date >= request.RequestDto.StartDate.Value);
                }

                if (request.RequestDto.EndDate.HasValue)
                {
                    query = query.Where(x => x.Date <= request.RequestDto.EndDate.Value);
                }
            }
            // Otherwise use period
            else if (request.RequestDto.Period.HasValue &&
                     request.RequestDto.Period != ProgressPeriod.All)
            {
                var today = DateTime.UtcNow;

                DateTime fromDate = request.RequestDto.Period switch
                {
                    ProgressPeriod.Week => today.AddDays(-7),
                    ProgressPeriod.Month => today.AddMonths(-1),
                    ProgressPeriod.Year => today.AddYears(-1),
                    _ => DateTime.MinValue
                };

                query = query.Where(x => x.Date >= fromDate);
            }

            var history = await query
                .OrderBy(x => x.Date)
                .Select(x => new WeightHistoryRecordDto
                {
                    Weight = x.Weight,
                    Date = x.Date
                })
                .ToListAsync(cancellationToken);

            return new GetWeightHistoryResponseDto
            {
                WeightHistory = history
            };
        }
    }
}