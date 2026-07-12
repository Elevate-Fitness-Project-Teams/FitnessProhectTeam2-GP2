using Elevate.Progress.Domain.Entities;
using Elevate.Progress.Features.LogWeightEntry.DTOS;
using Elevate.Progress.Infrastructure.Persistence.ProgressDbContext;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Elevate.Progress.Features.LogWeightEntry.Command
{
    public record CreateWeightHistoryCommand(CreateWeightHistoryRequestDto RequestDto)
         : IRequest<CreateWeightHistoryResponseDto>;

    public class CreateWeightHistoryCommandHandler
        : IRequestHandler<CreateWeightHistoryCommand, CreateWeightHistoryResponseDto>
    {
        private readonly ProgressDbContext _context;

        public CreateWeightHistoryCommandHandler(ProgressDbContext context)
        {
            _context = context;
        }

        public async Task<CreateWeightHistoryResponseDto> Handle(
            CreateWeightHistoryCommand request,
            CancellationToken cancellationToken)
        {
            var weightHistory = new WeightHistory
            {
                UserId = request.RequestDto.UserId,
                Weight = request.RequestDto.Weight,
                Date = request.RequestDto.Date,
                Notes = request.RequestDto.Notes
            };

            _context.WeightHistory.Add(weightHistory);

            await _context.SaveChangesAsync(cancellationToken);

            return new CreateWeightHistoryResponseDto
            {
                WeightHistoryId = weightHistory.Id
            };
        }
    }
}
