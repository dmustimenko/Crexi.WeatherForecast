using Newtonsoft.Json;

namespace Crexi.WeatherForecast.Models.Weather
{
	public class WeatherModel
	{
		[JsonProperty("city")]
		public string City { get; set; }

		[JsonProperty("temperature")]

		public double Temperature { get; set; }
	}
}