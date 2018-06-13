using System.Collections.Generic;
using Crexi.WeatherForecast.Models.Weather;

namespace Crexi.WeatherForecast.Services.Interfaces
{
	public interface IWeatherService
	{
		WeatherModel GetCurrentWeather(string city);

		IEnumerable<WeatherModel> GetForecastOnWeek(string city);
	}
}
