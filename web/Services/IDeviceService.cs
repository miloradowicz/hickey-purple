namespace web.Services;

public interface IDeviceService
{
  public Task RebootDevice(uint id);
  public Task RebootAllDevices();
}