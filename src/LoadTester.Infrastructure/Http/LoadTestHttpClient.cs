using LoadTester.Domain.Http;
using Microsoft.Extensions.Logging;

namespace LoadTester.Infrastructure.Http;

public class LoadTestHttpClient : ILoadTestHttpClient
{
    private readonly HttpClient httpClient;
    private readonly ILogger<LoadTestHttpClient> logger;

    public LoadTestHttpClient(HttpClient httpClient, ILogger<LoadTestHttpClient> logger)
    {
        this.httpClient = httpClient;
        this.logger = logger;
    }

    public async Task SendRequest()
    {
        this.logger.LogDebug("Sending request");

        // Simulating work
        await Task.Delay(50);
    }
}