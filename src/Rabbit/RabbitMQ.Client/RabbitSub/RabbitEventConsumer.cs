using System;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Extensions.Options;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;

namespace RabbitSub
{
    public class RabbitEventConsumer : IEventConsumer, IDisposable
    {
        private readonly IConnection _connection;
        private readonly IModel _channel;
        private bool _consuming;
        private readonly string _queueName;

        public RabbitEventConsumer(IOptions<SubConfig> consumerConfig)
        {
            var subConfig = consumerConfig.Value;

            var factory = new ConnectionFactory {HostName = subConfig.Hostname, DispatchConsumersAsync = true};
            _connection = factory.CreateConnection();
            _channel = _connection.CreateModel();
            _queueName = _channel.QueueDeclare().QueueName; // Create a non-durable, exclusive, autodelete queue with a generated name
            _channel.QueueBind(queue: _queueName,
                subConfig.Exchange,
                routingKey: "");
        }

        public Task StartConsuming()
        {
            if (_consuming)
                return Task.CompletedTask;

            var consumer = new AsyncEventingBasicConsumer(_channel);
            consumer.Received += ConsumerOnReceived;

            _channel.BasicQos(0, 10, false);
            _channel.BasicConsume(queue: _queueName,
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

            _channel.BasicAck(ea.DeliveryTag, false);

            await Task.Delay(1000);
        }

        public void Dispose()
        {
            _connection.Dispose(); // When a channel's connection is closed, so is the channel.
        }
    }
}