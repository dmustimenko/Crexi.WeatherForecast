using System.Collections.Generic;
using System.Web.Http;
using Crexi.WeatherForecast.Infrastructure;
using Crexi.WeatherForecast.Models.Weather;
using Crexi.WeatherForecast.Services.Interfaces;

namespace Crexi.WeatherForecast.Controllers
{
	[RoutePrefix("weather")]
	public class WeatherController : BaseController
	{
		#region IoC

		private readonly IWeatherService _weatherService;

		public WeatherController(IServiceInterceptor serviceInterceptor, IWeatherService weatherService)
			: base(serviceInterceptor)
		{
			_weatherService = weatherService;
		}

		#endregion

		#region Weather

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

		#endregion
	}
}
