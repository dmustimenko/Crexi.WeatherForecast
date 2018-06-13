using System;
using System.IO;
using System.Net;
using Crexi.WeatherForecast.App_Start;
using Crexi.WeatherForecast.Common.Logger;
using Crexi.WeatherForecast.Common.Serialization;
using Crexi.WeatherForecast.Services.Interfaces;

namespace Crexi.WeatherForecast.Services
{
	public class IpGeolocator: IIpGeolocator
	{
		private readonly ILogger _logger;

		public IpGeolocator(ILogger logger)
		{
			_logger = logger;
		}

		string IIpGeolocator.GetIpCountryCode(string ip)
		{
			try
			{
				string uri = $"{AppConfig.IpGeolocatorHost}/{ip}";
				var request = WebRequest.Create(uri);
				request.ContentType = "application/json; charset=utf-8";

				using (var response = (HttpWebResponse)request.GetResponse())
				{
					if (response.StatusCode != HttpStatusCode.OK)
					{
						return null;
					}

					var stream = response.GetResponseStream();
					if (stream == null)
					{
						return null;
					}

					using (var reader = new StreamReader(stream))
					{
						string json = reader.ReadToEnd();

						var ipGeoDetails = ObjectSerializer.ToObject<IpGeoDetails>(json);
						return ipGeoDetails?.CountryCode;
					}
				}
			}
			catch (Exception ex)
			{
				_logger.Error("Ip geolocator is not available", ex);
				return null;
			}
		}
	}
}