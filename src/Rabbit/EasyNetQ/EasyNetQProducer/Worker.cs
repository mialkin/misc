using System;
using System.Threading;
using System.Threading.Tasks;
using EasyNetQ;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

namespace EasyNetQProducer
{
    public class Worker : BackgroundService
    {
        private readonly ILogger<Worker> _logger;
        private readonly IBus _bus;

        public Worker(ILogger<Worker> logger)
        {
            _logger = logger;
            _bus = RabbitHutch.CreateBus("host=localhost");
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            while (!stoppingToken.IsCancellationRequested)
            {
                _logger.LogInformation("Worker running at: {time}", DateTimeOffset.Now);

                await _bus.PubSub.PublishAsync("Message", stoppingToken);
                await Task.Delay(1000, stoppingToken);
            }
        }
    }
}