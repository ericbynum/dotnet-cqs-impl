using System;
using System.Linq;
using Api.Cqs;
using Microsoft.Extensions.Logging;

namespace Api.Queries
{
    public interface IGetWeatherForecastQuery : IQuery
    {
        WeatherForecast[] Execute(int daysToForecast);
    }

    public class GetWeatherForecastQuery : IGetWeatherForecastQuery
    {
        private static readonly string[] Summaries = 
        {
            "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
        };

        private readonly ILogger<GetWeatherForecastQuery> _logger;

        public GetWeatherForecastQuery(ILogger<GetWeatherForecastQuery> logger)
        {
            _logger = logger;
        }

        public WeatherForecast[] Execute(int daysToForecast)
        {
            _logger.LogInformation("Query weather forecast.");

            var rng = new Random();
            return Enumerable.Range(1, daysToForecast).Select(index => new WeatherForecast
            {
                Date = DateTime.Now.AddDays(index),
                TemperatureC = rng.Next(-20, 55),
                Summary = Summaries[rng.Next(Summaries.Length)]
            })
            .ToArray();
        }
    }
}