using System.Web.Http;
using System.Web.Http.Filters;
using Crexi.WeatherForecast.Common.Logger;
using Crexi.WeatherForecast.Infrastructure;
using Crexi.WeatherForecast.Infrastructure.Cache;
using Crexi.WeatherForecast.Services;
using Crexi.WeatherForecast.Services.Interfaces;
using LightInject;
using IServiceContainer = LightInject.IServiceContainer;
using ServiceContainer = LightInject.ServiceContainer;

namespace Crexi.WeatherForecast.App_Start
{
	public class IocConfig
	{
		private IServiceContainer _diContainer;

		public IServiceContainer InitContainer(HttpConfiguration config)
		{
			_diContainer = new ServiceContainer();
			_diContainer.RegisterApiControllers();
			_diContainer.EnableWebApi(config);

			RegisterContainer();
			RegisterCache();
			RegisterFilters();
			RegisterLogger();
			RegisterServices();

			return _diContainer;
		}

		private void RegisterContainer()
		{
			_diContainer.RegisterInstance(typeof(IServiceFactory), _diContainer);
		}

		private void RegisterCache()
		{
			_diContainer.Register<ICacheManagerFactory, CacheManagerFactory>(new PerContainerLifetime());
		}

		private void RegisterFilters()
		{
			_diContainer.Register<IExceptionFilter, ExceptionFilter>(new PerRequestLifeTime());
			_diContainer.Register<IAuthorizationFilter, AuthorizationFilter>(new PerRequestLifeTime());
		}

		private void RegisterLogger()
		{
			_diContainer.Register<LoggerFactory>(new PerContainerLifetime());
			_diContainer.Register<ILogger>(factory => new Logger(factory.GetInstance<LoggerFactory>().CreateLogger()));
		}

		private void RegisterServices()
		{
			_diContainer.Register<IWeatherService, OpenWeather>(new PerRequestLifeTime());
			_diContainer.Register<IIpGeolocator, IpGeolocator>(new PerRequestLifeTime());
		}
	}
}