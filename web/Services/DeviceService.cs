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
    var device = await this.context.Devices.FindAsync(id);

    if (device is null)
    {
      throw new DeviceNotFoundException($"Could not find device by id {id}");
    }

    try
    {
      using var client = this.clientFactory.GetHttpClient(device.Address, device.Port, device.Username, device.Password);

      var result = await client.GetAsync(null as string, null);

      if (result.IsSuccessStatusCode)
      {
        return new DeviceStatus(device.Name, )
      }
    }
    catch (HttpRequestException)
    { }
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

  public async Task RebootDevice(int id)
  {
    var device = await this.context.Devices.FindAsync(id);

    if (device is null)
    {
      throw new DeviceNotFoundException($"Could not find device by id {id}");
    }

    try
    {
      using var client = this.clientFactory.GetHttpClient(device.Address, device.Port, device.Username, device.Password);

      var result = await client.PutAsync(null as string, null);

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