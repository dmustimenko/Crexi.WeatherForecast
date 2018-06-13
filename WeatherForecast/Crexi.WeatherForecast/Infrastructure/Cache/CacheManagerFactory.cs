using Crexi.WeatherForecast.App_Start;

namespace Crexi.WeatherForecast.Infrastructure.Cache
{
	public class CacheManagerFactory : ICacheManagerFactory
	{
		public ICacheManager CreateCacheManager()
		{
			return new CacheManager(
				AppConfig.CacheName,
				AppConfig.CacheEnable,
				AppConfig.CacheExpirationPointInterval
			);
		}
	}
}
