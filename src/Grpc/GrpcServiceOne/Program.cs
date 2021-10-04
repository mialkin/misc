using System;
using System.IO;
using GrpcServiceOne.Extensions;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;

namespace GrpcServiceOne
{
    public class Program
    {
        private static IConfigurationRoot? _configurationRoot;

        public static void Main(string[] args)
        {
            ConfigurationBuilder builder = new();
            BuildConfig(builder);

            _configurationRoot = builder.Build();
            if (_configurationRoot == null)
                throw new ArgumentNullException(nameof(_configurationRoot));

            CreateHostBuilder(args).Build().Run();
        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureWebHostDefaults(webBuilder =>
                {
                    webBuilder.ConfigureMacOsKestrel(_configurationRoot);
                    webBuilder.UseStartup<Startup>();
                });

        static void BuildConfig(IConfigurationBuilder builder)
        {
            builder.SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
                .AddJsonFile($"appsettings.{Environment.GetEnvironmentVariable("DOTNET_ENVIRONMENT") ?? "Production"}.json", optional: true,
                    reloadOnChange: true);
        }
    }
}