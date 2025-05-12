using Microsoft.EntityFrameworkCore;
using web.Models;
using web.Services;

Console.WriteLine(Environment.GetEnvironmentVariable("DOTNET_ENVIRONMENT"));

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddDbContext<PurpleContext>(opt => opt.UseNpgsql(builder.Configuration.GetConnectionString("PurpleConnection")));
builder.Services.AddTransient<web.Services.IHttpClientFactory, HttpClientFactory>();
builder.Services.AddTransient<IDeviceService, DeviceService>();

var app = builder.Build();

// Configure the HTTP request pipeline.

app.UseHttpsRedirection();

var summaries = new[]
{
    "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
};

app.MapPut("/reboot", async (IDeviceService rebootService) =>
{
    await rebootService.RebootAllDevices();
});

app.MapGet("/weatherforecast", () =>
{
    var forecast = Enumerable.Range(1, 5).Select(index =>
        new WeatherForecast
        (
            DateOnly.FromDateTime(DateTime.Now.AddDays(index)),
            Random.Shared.Next(-20, 55),
            summaries[Random.Shared.Next(summaries.Length)]
        ))
        .ToArray();
    return forecast;
});

app.Run();

record WeatherForecast(DateOnly Date, int TemperatureC, string? Summary)
{
    public int TemperatureF => 32 + (int)(TemperatureC / 0.5556);
}
