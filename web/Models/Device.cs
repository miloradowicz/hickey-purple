using System.ComponentModel.DataAnnotations;

namespace web.Models;

#nullable disable
public class Device
{
  public Device(string name, string address, uint port, string username, string password)
  {
    (this.Name, this.Address, this.Port, this.Username, this.Password) = (name, address, port, username, password);
  }

  [Key]
  public uint Id { get; set; }
  public string Name { get; set; }
  public string Address { get; set; }
  public uint Port { get; set; }
  public string Username { get; set; }
  public string Password { get; set; }
  public DateTime? LastReboot { get; set; }
}
