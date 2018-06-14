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
		public WeatherForecastModel Today(string city)
		{
			return _weatherService.GetCurrentWeather(city);
		}

		[HttpGet]
		[Route("week")]
		[Route("week/{city}")]
		public WeatherForecastModel Week(string city)
		{
			return _weatherService.GetForecastOnWeek(city);
		}

		#endregion
	}
}
