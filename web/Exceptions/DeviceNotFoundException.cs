namespace web.Exceptions;

public class DeviceNotFoundException : Exception
{
  public DeviceNotFoundException() { }

  public DeviceNotFoundException(string? message) : base(message) { }

  public DeviceNotFoundException(string? message, Exception? innerException) : base(message, innerException) { }
}