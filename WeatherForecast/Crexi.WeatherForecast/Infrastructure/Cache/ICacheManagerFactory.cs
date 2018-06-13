namespace Crexi.WeatherForecast.Infrastructure.Cache
{
	public interface ICacheManagerFactory
	{
		Infrastructure.Cache.ICacheManager CreateCacheManager();
	}
}
