using System;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Extensions.Options;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;

namespace RabbitConsumerWorker
{
    public class RabbitEventConsumer : IEventConsumer, IDisposable
    {
        private readonly ConsumerConfig _consumerConfig;
        private readonly IConnection _connection;
        private readonly IModel _channel;
        private bool _consuming;

        public RabbitEventConsumer(IOptions<ConsumerConfig> consumerConfig)
        {
            _consumerConfig = consumerConfig.Value;

            var factory = new ConnectionFactory {HostName = _consumerConfig.Hostname, DispatchConsumersAsync = true};
            _connection = factory.CreateConnection();
            _channel = _connection.CreateModel();
            
            _channel.QueueDeclare(queue: _consumerConfig.Queue,
                durable: true,
                exclusive: false,
                autoDelete: false,
                arguments: null);
        }

        public Task StartConsuming()
        {
            if (_consuming)
                return Task.CompletedTask;

            var consumer = new AsyncEventingBasicConsumer(_channel);
            consumer.Received += ConsumerOnReceived;

            _channel.BasicConsume(queue: _consumerConfig.Queue,
                autoAck: false,
                consumer: consumer);

            _consuming = true;
            return Task.CompletedTask;
        }

        private async Task ConsumerOnReceived(object? sender, BasicDeliverEventArgs ea)
        {
            var body = ea.Body.ToArray();
            var message = Encoding.UTF8.GetString(body);
            Console.WriteLine(message);
            
            //_channel.BasicReject(ea.DeliveryTag, false);
            _channel.BasicAck(ea.DeliveryTag, false);

            await Task.Delay(10);

        }

        public void Dispose()
        {
            _connection.Dispose(); // When a channel's connection is closed, so is the channel.
        }
    }
}