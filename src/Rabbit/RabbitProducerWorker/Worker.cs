using System.Text;
using System.Text.Json;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

namespace RabbitProducerWorker
{
    public class Worker : BackgroundService
    {
        private readonly ILogger<Worker> _logger;
        private readonly IEventPublisher _eventPublisher;
        private readonly IEventModelProvider _eventModelProvider;

        public Worker(ILogger<Worker> logger, IEventPublisher eventPublisher, IEventModelProvider eventModelProvider)
        {
            _logger = logger;
            _eventPublisher = eventPublisher;
            _eventModelProvider = eventModelProvider;
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            while (!stoppingToken.IsCancellationRequested)
            {
                EventModel model = _eventModelProvider.GetEventModel();
                string message = JsonSerializer.Serialize(model);
                // ReSharper disable once TemplateIsNotCompileTimeConstantProblem
                _logger.LogInformation(message);

                _eventPublisher.Publish(Encoding.UTF8.GetBytes(message));
                await Task.Delay(1000, stoppingToken);
            }
        }
    }
}