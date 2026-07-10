using Elevate.Progress.Features.LogWeightEntry.Events.DTOS;
using MediatR;

namespace Elevate.Progress.Features.LogWeightEntry.Events.Event
{
    public record WeightUpdatedIntegrationEvent(
        WeightUpdatedEventDto EventDto)
        : INotification;

    public class WeightUpdatedIntegrationEventHandler
        : INotificationHandler<WeightUpdatedIntegrationEvent>
    {
        public async Task Handle(
            WeightUpdatedIntegrationEvent notification,
            CancellationToken cancellationToken)
        {
            // TODO:
            // Publish weight_updated integration event
            // to the FCE Service through the Message Broker.

            await Task.CompletedTask;
        }
    }
}