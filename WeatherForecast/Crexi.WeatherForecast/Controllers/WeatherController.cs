using System.Collections.Generic;
using System.Web.Http;
using Crexi.WeatherForecast.Models.Weather;
using Crexi.WeatherForecast.Services;

namespace Crexi.WeatherForecast.Controllers
{
	[RoutePrefix("weather")]
	public class WeatherController : ApiController
	{
		private readonly IWeatherService _weatherService;

		public WeatherController(IWeatherService weatherService)
		{
			_weatherService = weatherService;
		}

		[HttpGet]
		[Route("today")]
		[Route("today/{city}")]
		public WeatherModel Today(string city)
		{
			return _weatherService.GetCurrentWeather(city);
		}

		[HttpGet]
		[Route("week")]
		[Route("week/{city}")]
		public IEnumerable<WeatherModel> Week(string city)
		{
			return _weatherService.GetForecastOnWeek(city);
		}
	}
}
