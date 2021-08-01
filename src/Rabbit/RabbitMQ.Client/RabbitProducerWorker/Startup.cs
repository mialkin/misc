using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace RabbitProducerWorker
{
    public static class Startup
    {
        public static void ConfigureServices(HostBuilderContext hostContext, IServiceCollection services)
        {
            services.Configure<ProducerConfig>(hostContext.Configuration.GetSection(nameof(ProducerConfig)));
            services.AddHostedService<Worker>();

            services.AddSingleton<IEventModelProvider, EventModelProvider>();
            services.AddSingleton<IEventProducer, RabbitProducer>();
        }
    }
}