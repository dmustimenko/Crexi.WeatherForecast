using Crexi.WeatherForecast.Models.Weather;

namespace Crexi.WeatherForecast.Services.Interfaces
{
	public interface IWeatherService
	{
		WeatherForecastModel GetCurrentWeather(string city);

		WeatherForecastModel GetForecastOnWeek(string city);
	}
}
