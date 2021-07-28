using System.Text;
using System.Text.Json;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using RabbitMQ.Client;

namespace RabbitProducerWorker
{
    public class Worker : BackgroundService
    {
        private readonly ILogger<Worker> _logger;
        private static IModel? _channel;
        private readonly ProducerConfig _producerConfig;

        public Worker(ILogger<Worker> logger, IOptions<ProducerConfig> producerConfigOptions)
        {
            _logger = logger;
            _producerConfig = producerConfigOptions.Value;

            var factory = new ConnectionFactory {HostName = _producerConfig.Hostname};
            var connection = factory.CreateConnection();
            _channel = connection.CreateModel();
            _channel.QueueDeclare(queue: _producerConfig.Queue, durable: true, exclusive: false, autoDelete: false,
                arguments: null);
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            while (!stoppingToken.IsCancellationRequested)
            {
                EventModel eventModel = EventModelGetter.GetEventModel();
                string message = JsonSerializer.Serialize(eventModel);
                // ReSharper disable once TemplateIsNotCompileTimeConstantProblem
                _logger.LogInformation(message);

                var body = Encoding.UTF8.GetBytes(message);

                var properties = _channel.CreateBasicProperties();
                properties.Persistent = true;
                _channel.BasicPublish(exchange: "", routingKey: _producerConfig.RoutingKey, basicProperties: properties,
                    body: body);

                await Task.Delay(1000, stoppingToken);
            }
        }
    }
}