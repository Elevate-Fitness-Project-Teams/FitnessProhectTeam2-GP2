namespace Elevate.Progress.Integration.Publishers
{
    public interface IIntegrationEventPublisher
    {
        Task PublishAsync<TEvent>(TEvent integrationEvent, CancellationToken cancellationToken = default);
    }
}
