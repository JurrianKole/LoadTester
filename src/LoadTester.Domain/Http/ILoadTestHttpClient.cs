namespace LoadTester.Domain.Http;

public interface ILoadTestHttpClient
{
    Task SendRequest();
}