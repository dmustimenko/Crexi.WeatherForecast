using System;
using System.Configuration;

namespace Crexi.WeatherForecast.App_Start
{
	public class AppConfig
	{
		#region Open Weather Api

		public static string OpenWeatherApiKey => ConfigurationManager.AppSettings["OpenWeather.ApiKey"];

		#endregion

		#region Access

		public static int AccessTimeout => int.Parse(ConfigurationManager.AppSettings["Access.TimeoutInSec"]);
		public static int AccessAttemptCount => int.Parse(ConfigurationManager.AppSettings["Access.AttemptCount"]);
		public static string[] AccessValidCountryCodes
		{
			get
			{
				string countryCodes = ConfigurationManager.AppSettings["Access.ValidCountryCodes"];
				return countryCodes.Split(new[] { "," }, StringSplitOptions.RemoveEmptyEntries);
			}
		}

		#endregion

		#region Ip Geolocator

		public static string IpGeolocatorHost => ConfigurationManager.AppSettings["IpGeolocator.Host"];

		#endregion

		#region Cache

		public static bool CacheEnable => bool.Parse(ConfigurationManager.AppSettings["Cache.Enable"]);

		public static string CacheName => ConfigurationManager.AppSettings["Cache.Name"];

		public static TimeSpan CacheExpirationPointInterval => TimeSpan.Parse(ConfigurationManager.AppSettings["Cache.ExpirationPointInterval"]);

		#endregion
	}
}