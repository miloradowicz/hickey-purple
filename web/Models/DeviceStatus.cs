namespace web.Models;

public record DeviceStatus(
  string Name,
  DeviceStatusEnum Status,
  DateTime? LastReboot
);