using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace RabbitPub
{
    public static class Startup
    {
        public static void ConfigureServices(HostBuilderContext hostContext, IServiceCollection services)
        {
            services.Configure<PubConfig>(hostContext.Configuration.GetSection(nameof(PubConfig)));
            services.AddHostedService<Worker>();

            services.AddSingleton<IEventModelProvider, EventModelProvider>();
            services.AddSingleton<IEventProducer, RabbitProducer>();
        }
    }
}