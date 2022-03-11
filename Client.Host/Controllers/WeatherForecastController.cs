using System;
using System.Linq;
using System.Threading.Tasks;
using Client.Pages;
using Microsoft.AspNetCore.Mvc;

namespace Client.Host.Controllers;

[ApiController]
[Route("[controller]")]
public class WeatherForecastController : ControllerBase
{
    public async Task<IActionResult> Get()
    {
        var random = new Random();

        var forecasts = Enumerable
            .Range(1, 5)
            .Select(index => new FetchData.WeatherForecast
        {
            Date = DateTime.Now.AddDays(index),
            TemperatureC = random.Next(-20, 55),
            Summary = Summaries[random.Next(Summaries.Length)]
        });

        return Ok(forecasts);
    }
    
    private static readonly string[] Summaries = new[]
    {
        "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
    };
}