using LoadTester.Infrastructure.Http;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

namespace LoadTester.Console;

internal static class ServiceConfigurator
{
    internal static IServiceProvider ConfigureServices(IServiceCollection services)
    {
        services.AddLogging(config =>
        {
            config.SetMinimumLevel(LogLevel.Debug);
            config.AddDebug();
            config.AddConsole();
        });

        services.AddHttpClient<LoadTestHttpClient>();

        return services.BuildServiceProvider();
    }
}