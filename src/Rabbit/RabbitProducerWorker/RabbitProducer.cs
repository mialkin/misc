using System;
using Microsoft.Extensions.Options;
using RabbitMQ.Client;

namespace RabbitProducerWorker
{
    public class RabbitProducer : IEventProducer, IDisposable
    {
        private readonly ProducerConfig _producerConfig;
        private static IConnection _connection;
        private static IModel? _channel; // https://www.rabbitmq.com/channels.html

        public RabbitProducer(IOptions<ProducerConfig> producerConfigOptions)
        {
            _producerConfig = producerConfigOptions.Value;

            var factory = new ConnectionFactory {HostName = _producerConfig.Hostname};
            
            _connection = factory.CreateConnection();
            _channel = _connection.CreateModel(); 
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

        public void Dispose()
        {
            _connection.Dispose(); // When a channel's connection is closed, so is the channel.
        }
    }
}