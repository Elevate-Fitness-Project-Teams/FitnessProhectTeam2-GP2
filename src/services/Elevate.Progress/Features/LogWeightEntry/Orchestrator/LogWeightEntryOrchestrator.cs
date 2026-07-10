using Elevate.Progress.Features.LogWeightEntry.Command;
using Elevate.Progress.Features.LogWeightEntry.DTOS;
using Elevate.Progress.Features.LogWeightEntry.Events.DTOS;
using Elevate.Progress.Features.LogWeightEntry.Events.Event;
using Elevate.Progress.Features.LogWeightEntry.Exception;
using Elevate.Progress.Features.LogWeightEntry.Query;
using MediatR;

namespace Elevate.Progress.Features.LogWeightEntry.Orchestrator
{
    public record LogWeightEntryOrchestrator(LogWeightEntryRequestDto RequestDto)
        : IRequest<LogWeightEntryResponseDto>;

    public class LogWeightEntryOrchestratorHandler
        : IRequestHandler<LogWeightEntryOrchestrator, LogWeightEntryResponseDto>
    {
        private readonly IMediator _mediator;

        public LogWeightEntryOrchestratorHandler(IMediator mediator)
        {
            _mediator = mediator;
        }

        public async Task<LogWeightEntryResponseDto> Handle(
            LogWeightEntryOrchestrator request,
            CancellationToken cancellationToken)
        {
            // 0. Validation
            if (request.RequestDto.Weight < 40 || request.RequestDto.Weight > 200)
                throw new InvalidWeightException();

            if (request.RequestDto.Date == default)
                throw new InvalidDateException();

            // 1. Get previous weight
            var previousWeightResult = await _mediator.Send(
                new GetPreviousWeightQuery(
                    new GetPreviousWeightRequestDto
                    {
                        UserId = request.RequestDto.UserId,
                        Date = request.RequestDto.Date
                    }),
                cancellationToken);

            var previousWeight = previousWeightResult.PreviousWeight;

            // 2. Calculate difference
            decimal differenceFromPrevious = 0;

            if (previousWeight.HasValue)
            {
                differenceFromPrevious =
                    previousWeight.Value - request.RequestDto.Weight;
            }

            // 3. Create weight history
            await _mediator.Send(
                new CreateWeightHistoryCommand(
                    new CreateWeightHistoryRequestDto
                    {
                        UserId = request.RequestDto.UserId,
                        Weight = request.RequestDto.Weight,
                        Date = request.RequestDto.Date,
                        Notes = request.RequestDto.Notes
                    }),
                cancellationToken);

            // 4. Update user statistics
            var statsResult = await _mediator.Send(
                new UpdateUserStatisticsAfterWeightCommand(
                    new UpdateUserStatisticsAfterWeightRequestDto
                    {
                        UserId = request.RequestDto.UserId,
                        WeightDifference = differenceFromPrevious 
                    }),
                cancellationToken);

            // 5. Publish integration event
            await _mediator.Publish(
                new WeightUpdatedIntegrationEvent(
                    new WeightUpdatedEventDto
                    {
                        UserId = request.RequestDto.UserId,
                        Weight = request.RequestDto.Weight,
                        Date = request.RequestDto.Date
                    }),
                cancellationToken);

            // 6. Return response
            return new LogWeightEntryResponseDto
            {
                Bmi = null, // سيتم إرجاعه من FCE لاحقاً
                DifferenceFromPrevious = differenceFromPrevious, 
                TotalWeightLost = statsResult.TotalWeightLost, 
                Success = true
            }; 
        }
    }
}