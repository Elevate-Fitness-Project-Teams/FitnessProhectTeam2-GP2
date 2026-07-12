namespace Elevate.Progress.Integration.Publishers
{
    public interface IRabbitMqPublisher
    {
        Task PublishEventAsync<T>(
        T @event,
        string exchange,
        string routingKey,
        CancellationToken cancellationToken = default)
        where T : class;
    }
}

