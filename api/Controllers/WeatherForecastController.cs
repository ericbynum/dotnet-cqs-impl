using Api.Cqs;
using Api.Queries;
using Microsoft.AspNetCore.Mvc;

namespace Api.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class WeatherForecastController : ControllerBase
    {
        private readonly ICqsResolver _cqsResolver;

        public WeatherForecastController(ICqsResolver cqsResolver)
        {
            _cqsResolver = cqsResolver;
        }

        [HttpGet]
        [ProducesResponseType(typeof(WeatherForecast[]), 200)]
        public IActionResult Get()
        {
            var query = _cqsResolver.ResolveQuery<IGetWeatherForecastQuery>();
            WeatherForecast[] forecasts = query.Execute(daysToForecast: 5);
            return Ok(forecasts);
        }
    }
}