using System;
using Microsoft.Extensions.Options;
using RabbitMQ.Client;

namespace RabbitPub
{
    public class RabbitProducer : IEventProducer, IDisposable
    {
        private readonly PubConfig _pubConfig;
        private static IConnection _connection;
        private static IModel? _channel;

        public RabbitProducer(IOptions<PubConfig> producerConfigOptions)
        {
            _pubConfig = producerConfigOptions.Value;

            var factory = new ConnectionFactory {HostName = _pubConfig.Hostname};
            
            _connection = factory.CreateConnection();
            _channel = _connection.CreateModel(); 
            _channel.ExchangeDeclare(_pubConfig.Exchange, ExchangeType.Fanout);
        }

        public void Publish(byte[] message)
        {
            var properties = _channel.CreateBasicProperties();
            properties.Persistent = true;
            _channel.BasicPublish(
                exchange: _pubConfig.Exchange,
                routingKey: "",
                basicProperties: properties,
                body: message);
        }

        public void Dispose()
        {
            _connection.Dispose(); // When a channel's connection is closed, so is the channel.
        }
    }
}