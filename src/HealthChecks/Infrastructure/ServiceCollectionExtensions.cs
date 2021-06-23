using System;
using HealthChecks.Configs;
using Microsoft.Extensions.Configuration;
using StackExchange.Redis;

// ReSharper disable once CheckNamespace
namespace Microsoft.Extensions.DependencyInjection
{
    public static class ServiceCollectionExtensions
    {
        public static void ConfigureRedis(this IServiceCollection services, IConfiguration configuration)
        {
            RedisConfig config = configuration.GetSection("Redis").Get<RedisConfig>();
            if (config == null || string.IsNullOrWhiteSpace(config.ConnectionString))
                throw new ArgumentException("Redis connection string must be set.", nameof(RedisConfig));

            services.AddSingleton<IConnectionMultiplexer>(x => ConnectionMultiplexer.Connect(config.ConnectionString));
        }
    }
}