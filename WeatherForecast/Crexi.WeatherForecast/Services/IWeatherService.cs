using System.Collections.Generic;
using Crexi.WeatherForecast.Models.Weather;

namespace Crexi.WeatherForecast.Services
{
	public interface IWeatherService
	{
		WeatherModel GetCurrentWeather(string city);

		IEnumerable<WeatherModel> GetForecastOnWeek(string city);
	}
}
