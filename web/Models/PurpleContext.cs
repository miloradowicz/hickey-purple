using Microsoft.EntityFrameworkCore;

namespace web.Models;

public class PurpleContext(DbContextOptions<PurpleContext> options) : DbContext(options)
{
  public DbSet<Device> Devices { get; set; }
}