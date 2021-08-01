using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace RabbitConsumerWorker
{
    public static class Startup
    {
        public static void ConfigureServices(HostBuilderContext hostContext, IServiceCollection services)
        {
            services.Configure<ConsumerConfig>(hostContext.Configuration.GetSection(nameof(ConsumerConfig)));
            services.AddHostedService<Worker>();

            services.AddSingleton<IEventConsumer, RabbitEventConsumer>();
        }
    }
}