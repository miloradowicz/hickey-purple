namespace web.Services;

public interface IDeviceService
{
  public Task Reboot();
  public Task<bool> Reboot(string address, uint port, string username, string password);
}