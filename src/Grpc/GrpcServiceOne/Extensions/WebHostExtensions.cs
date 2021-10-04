using System;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Server.Kestrel.Core;
using Microsoft.Extensions.Configuration;

namespace GrpcServiceOne.Extensions
{
    public static class WebHostExtensions
    {
        public static void ConfigureMacOsKestrel(this IWebHostBuilder builder, IConfigurationRoot configurationRoot)
        {
            int port = configurationRoot.GetValue<int>("Port");

            if (port <= 0)
                throw new ArgumentOutOfRangeException(nameof(port));

            string? environmentVariable = Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT");

            if (!string.IsNullOrWhiteSpace(environmentVariable) &&
                environmentVariable.Equals("Development", StringComparison.InvariantCultureIgnoreCase))
            {
                builder.ConfigureKestrel(options => options.ListenLocalhost(port, x => x.Protocols = HttpProtocols.Http2));
            }
        }
    }
}