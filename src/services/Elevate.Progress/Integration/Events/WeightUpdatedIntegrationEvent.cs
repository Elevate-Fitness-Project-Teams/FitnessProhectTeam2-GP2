using Elevate.Progress.Integration.Publishers;
using Elevate.Progress.Integration.RabbitMQ;
using MediatR;

namespace Elevate.Progress.Integration.Events;

public sealed record WeightUpdatedIntegrationEvent(
    WeightUpdatedEvent EventData
) : INotification;

public sealed class WeightUpdatedIntegrationEventHandler
    : INotificationHandler<WeightUpdatedIntegrationEvent>
{
    private readonly IRabbitMqPublisher _publisher;

    public WeightUpdatedIntegrationEventHandler(
        IRabbitMqPublisher publisher)
    {
        _publisher = publisher;
    }

    public async Task Handle(
        WeightUpdatedIntegrationEvent notification,
        CancellationToken cancellationToken)
    {
        await _publisher.PublishEventAsync(
            notification.EventData,
            RabbitMqConstants.Exchange,
            RabbitMqConstants.RoutingKeys.WeightUpdated,
            cancellationToken);
    }
}
