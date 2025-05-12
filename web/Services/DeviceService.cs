using web.Exceptions;
using web.Models;

namespace web.Services;

public class DeviceService : IDeviceService
{
  private readonly PurpleContext context;
  private readonly IHttpClientFactory clientFactory;
  private readonly ILogger<DeviceService> logger;

  public DeviceService(PurpleContext context, IHttpClientFactory clientFactory, ILogger<DeviceService> logger)
  {
    this.context = context;
    this.clientFactory = clientFactory;
    this.logger = logger;
  }

  public async Task<DeviceStatus> GetDeviceStatus(int id)
  {
    var device = await this.context.Devices.FindAsync(id) ?? throw new DeviceNotFoundException($"Could not find device by id {id}");
    
    try
    {
      using var client = this.clientFactory.GetHttpClient(device.Address, device.Port, device.Username, device.Password);

      var result = await client.GetAsync("System/capabilities");

      if (result.IsSuccessStatusCode)
      {
        return new DeviceStatus(device.Name, DeviceStatusEnum.Up, device.LastReboot);
      }
      else
      {
        return new DeviceStatus(device.Name, DeviceStatusEnum.Down, device.LastReboot);
      }
    }
    catch (HttpRequestException)
    {
      return new DeviceStatus(device.Name, DeviceStatusEnum.RequestFailed, device.LastReboot);
    }
  }

  public async Task RebootAllDevices()
  {
    foreach (var device in this.context.Devices)
    {
      try
      {
        await this.RebootDevice(device.Id);
      }
      catch (DeviceNotFoundException)
      { }
    }
  }

  public async Task RebootDevice(uint id)
  {
    var device = await this.context.Devices.FindAsync(id);

    if (device is null)
    {
      throw new DeviceNotFoundException($"Could not find device by id {id}");
    }

    try
    {
      using var client = this.clientFactory.GetHttpClient(device.Address, device.Port, device.Username, device.Password);

      var result = await client.PutAsync("reboot", null);

      if (result.IsSuccessStatusCode)
      {
        var lastReboot = new DateTime();
        device.LastReboot = lastReboot;

        await this.context.SaveChangesAsync();
      }
    }
    catch (HttpRequestException)
    { }
  }
}