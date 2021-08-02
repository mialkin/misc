using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace RabbitSub
{
    public static class Startup
    {
        public static void ConfigureServices(HostBuilderContext hostContext, IServiceCollection services)
        {
            services.Configure<SubConfig>(hostContext.Configuration.GetSection(nameof(SubConfig)));
            services.AddHostedService<Worker>();

            services.AddSingleton<IEventConsumer, RabbitEventConsumer>();
        }
    }
}