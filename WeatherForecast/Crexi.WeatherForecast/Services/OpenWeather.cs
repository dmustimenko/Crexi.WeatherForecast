using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Crexi.WeatherForecast.App_Start;
using Crexi.WeatherForecast.Models.Weather;
using OpenWeatherMap;

namespace Crexi.WeatherForecast.Services
{
	public class OpenWeather : IWeatherService
	{
		private readonly OpenWeatherMapClient _client;

		public OpenWeather()
		{
			_client = new OpenWeatherMapClient(AppConfig.OpenWeatherApiKey);
		}

		WeatherModel IWeatherService.GetCurrentWeather(string city)
		{
			var weather = CurrentWeatherResponse(city);

			return new WeatherModel
			{
				City = city,
				Temperature = weather.Temperature.Value
			};
		}

		IEnumerable<WeatherModel> IWeatherService.GetForecastOnWeek(string city)
		{
			var response = ForecastOnWeekResponse(city);

			return response
				.Forecast
				.Select(weather => new WeatherModel
				{
					City = city,
					Temperature = weather.Temperature.Value
				}).ToList();
		}

		public CurrentWeatherResponse CurrentWeatherResponse(string city)
		{
			var task = Task.Run(() => _client.CurrentWeather.GetByName(city, MetricSystem.Metric));
			task.Wait();
			return task.Result;
		}

		public ForecastResponse ForecastOnWeekResponse(string city)
		{
			var task = Task.Run(() => _client.Forecast.GetByName(city, false, MetricSystem.Metric));
			task.Wait();
			return task.Result;
		}
	}
}