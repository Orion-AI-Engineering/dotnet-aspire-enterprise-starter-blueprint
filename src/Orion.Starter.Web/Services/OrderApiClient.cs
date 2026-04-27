namespace Orion.Starter.Web.Services;

public sealed class OrderApiClient(HttpClient httpClient)
{
    public HttpClient HttpClient { get; } = httpClient;
}
