using Elevate.Progress.Features.LogWeightEntry.Command;
using Elevate.Progress.Features.LogWeightEntry.DTOS;
using Elevate.Progress.Features.LogWeightEntry.Exception;
using Elevate.Progress.Features.LogWeightEntry.Query;
using Elevate.Progress.Integration.Clients;
using Elevate.Progress.Integration.Events;
using Elevate.Progress.Integration.Publishers;
using Elevate.Progress.Integration.RabbitMQ;
using MediatR;

namespace Elevate.Progress.Features.LogWeightEntry.Orchestrator
{
    public record LogWeightEntryOrchestrator(
        LogWeightEntryRequestDto RequestDto)
        : IRequest<LogWeightEntryResponseDto>;

    public class LogWeightEntryOrchestratorHandler
        : IRequestHandler<LogWeightEntryOrchestrator, LogWeightEntryResponseDto>
    {
        private readonly IMediator _mediator;
        private readonly IFitnessClient _fitnessClient;
        private readonly IRabbitMqPublisher _rabbitMqPublisher;

        public LogWeightEntryOrchestratorHandler(
            IMediator mediator,
            IFitnessClient fitnessClient,
            IRabbitMqPublisher rabbitMqPublisher)
        {
            _mediator = mediator;
            _fitnessClient = fitnessClient;
            _rabbitMqPublisher = rabbitMqPublisher;
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

            // 5. Publish Weight Updated Event
            await _rabbitMqPublisher.PublishEventAsync(
                new WeightUpdatedEvent(
                    request.RequestDto.UserId,
                    request.RequestDto.Weight,
                    request.RequestDto.Date),
                RabbitMqConstants.FitnessExchange,
                RabbitMqConstants.RoutingKeys.WeightUpdated,
                cancellationToken);

            // 6. Get Height from FCE
            var stats = await _fitnessClient.GetFitnessStatsAsync(
                request.RequestDto.UserId,
                cancellationToken);

            decimal? bmi = null;

            if (stats is not null && stats.Height > 0)
            {
                var heightInMeters = stats.Height / 100m;
                bmi = request.RequestDto.Weight / (heightInMeters * heightInMeters);
                bmi = Math.Round(bmi.Value, 2);
            }

            // 7. Return response
            return new LogWeightEntryResponseDto
            {
                Bmi = bmi,
                DifferenceFromPrevious = differenceFromPrevious,
                TotalWeightLost = statsResult.TotalWeightLost,
                Success = true
            };
        }
    }
}