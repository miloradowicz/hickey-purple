namespace web.Services;

public interface IHttpClientFactory
{
  public HttpClient GetHttpClient(string address, uint port, string username, string password);
}