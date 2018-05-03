using System.Configuration;

namespace Crexi.WeatherForecast.App_Start
{
	public class AppConfig
	{
		public static string OpenWeatherApiKey => ConfigurationManager.AppSettings["OpenWeather.ApiKey"];
	}
}