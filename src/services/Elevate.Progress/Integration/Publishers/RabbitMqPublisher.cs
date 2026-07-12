using Elevate.Progress.Integration.Configuration;
using Microsoft.Extensions.Options;
using RabbitMQ.Client;
using System.Text;
using System.Text.Json;

namespace Elevate.Progress.Integration.Publishers
{
    public class RabbitMqPublisher : IRabbitMqPublisher
    {
        private readonly RabbitMqOptions _options;

        public RabbitMqPublisher(IOptions<RabbitMqOptions> options)
        {
            _options = options.Value;
        }

        public async Task PublishEventAsync<T>(
                 T @event,
                 string exchange,
                 string routingKey,
                 CancellationToken cancellationToken = default)
                 where T : class
        {
            var factory = new ConnectionFactory
            {
                HostName = _options.HostName,
                Port = _options.Port,
                UserName = _options.UserName,
                Password = _options.Password,
                VirtualHost = _options.VirtualHost
            };

            await using var connection =
                await factory.CreateConnectionAsync(cancellationToken);

            await using var channel =
                await connection.CreateChannelAsync(cancellationToken: cancellationToken);

            await channel.ExchangeDeclareAsync(
                exchange,
                ExchangeType.Topic,
                durable: true,
                cancellationToken: cancellationToken);

            var body = Encoding.UTF8.GetBytes(
                JsonSerializer.Serialize(@event));

            await channel.BasicPublishAsync(
                exchange: exchange,
                routingKey: routingKey,
                body: body,
                cancellationToken: cancellationToken);
        }
    }
}
