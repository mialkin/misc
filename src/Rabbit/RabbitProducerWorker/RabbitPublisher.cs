using Microsoft.Extensions.Options;
using RabbitMQ.Client;

namespace RabbitProducerWorker
{
    public class RabbitPublisher : IEventPublisher
    {
        private readonly ProducerConfig _producerConfig;
        private static IModel? _channel;

        public RabbitPublisher(IOptions<ProducerConfig> producerConfigOptions)
        {
            _producerConfig = producerConfigOptions.Value;

            var factory = new ConnectionFactory {HostName = _producerConfig.Hostname};
            var connection = factory.CreateConnection();
            _channel = connection.CreateModel();
            _channel.QueueDeclare(
                queue: _producerConfig.Queue,
                durable: true,
                exclusive: false,
                autoDelete: false,
                arguments: null);
        }

        public void Publish(byte[] message)
        {
            var properties = _channel.CreateBasicProperties();
            properties.Persistent = true;
            _channel.BasicPublish(
                exchange: "",
                routingKey: _producerConfig.RoutingKey,
                basicProperties: properties,
                body: message);
        }
    }
}