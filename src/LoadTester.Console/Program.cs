using System.CommandLine;

using LoadTester.Domain;

var rootCommand = ConfigureRootCommand();

await rootCommand.InvokeAsync(args);

Console.WriteLine("Hello, World!");

static RootCommand ConfigureRootCommand()
{
    var targetUriOption = new Option<Uri>(
        name: "--targetUri",
        description: "Target Uri to load test");

    var requestsPerSecondOption = new Option<int>(
        name: "--requestsPerSecond",
        description: "Amount of requests per second");

    var testDurationOption = new Option<int>(
        name: "--testDuration",
        description: "Duration of the load test in seconds");

    var rootCommand = new RootCommand("LoadTester");

    var loadTestCommand = new Command("LoadTest", "Required configuration for the load test application")
    {
        targetUriOption,
        requestsPerSecondOption,
        testDurationOption
    };

    rootCommand.AddCommand(loadTestCommand);

    rootCommand.SetHandler((targetUri, requestsPerSecond, testDuration) =>
        {
            var targetConfiguration = TargetConfiguration
                .Create(
                    targetUri,
                    requestsPerSecond,
                    TimeSpan.FromSeconds(testDuration));
        },
        targetUriOption,
        requestsPerSecondOption,
        testDurationOption);

    return rootCommand;
}