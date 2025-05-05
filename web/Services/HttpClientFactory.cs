using System.Net;

namespace web.Services;

public class HttpClientFactory(ILogger<HttpClientFactory> logger) : IHttpClientFactory
{
  private readonly ILogger<HttpClientFactory> logger = logger;

  public HttpClient GetHttpClient(string address, uint port, string username, string password)
  {
    this.logger.LogDebug("Created client for {address}:{port}", address, port);

    return new HttpClient(new HttpClientHandler()
    {
      Credentials = new NetworkCredential(username, password)
    })
    {
      BaseAddress = new Uri($"http://${address}:${port}/ISAPI/reboot")
    };
  }
}