using System.CommandLine;
using LoadTester.Console;
using LoadTester.Domain;
using LoadTester.Infrastructure.Http;

using Microsoft.Extensions.DependencyInjection;

var serviceCollection = new ServiceCollection();

ServiceConfigurator.ConfigureServices(serviceCollection);

var serviceProvider = serviceCollection.BuildServiceProvider();

var rootCommand = ConfigureRootCommand(serviceProvider);

await rootCommand.InvokeAsync(args);

static RootCommand ConfigureRootCommand(IServiceProvider provider)
{
    var targetUriOption = new Option<Uri>(
        name: "--target",
        description: "Target Uri to load test");

    var requestsPerSecondOption = new Option<int>(
        name: "--requestsPerSecond",
        description: "Amount of requests per second");

    var testDurationOption = new Option<int>(
        name: "--testDuration",
        description: "Duration of the load test in seconds");

    var rootCommand = new RootCommand("LoadTester");

    var loadTestCommand = new Command("loadtest", "Required configuration for the load test application")
    {
        targetUriOption,
        requestsPerSecondOption,
        testDurationOption
    };

    rootCommand.AddCommand(loadTestCommand);

    loadTestCommand.SetHandler(async (targetUri, requestsPerSecond, testDuration) =>
        {
            var targetConfiguration = TargetConfiguration
                .Create(
                    targetUri,
                    requestsPerSecond,
                    TimeSpan.FromSeconds(testDuration));

            var loadTestHttpClient = provider.GetService<LoadTestHttpClient>();

            await loadTestHttpClient.SendRequest();
        },
        targetUriOption,
        requestsPerSecondOption,
        testDurationOption);

    return rootCommand;
}