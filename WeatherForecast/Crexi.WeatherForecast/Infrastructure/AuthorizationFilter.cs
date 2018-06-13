using System;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using System.Web;
using System.Web.Http.Controllers;
using System.Web.Http.Filters;
using Crexi.WeatherForecast.App_Start;
using Crexi.WeatherForecast.Infrastructure.Cache;
using Crexi.WeatherForecast.Models;
using Crexi.WeatherForecast.Services;
using Crexi.WeatherForecast.Services.Interfaces;
using Crexi.WeatherForecast.Shared.Enums;
using Microsoft.Owin;

namespace Crexi.WeatherForecast.Infrastructure
{
	public class AuthorizationFilter : IAuthorizationFilter
	{
		private readonly IIpGeolocator _ipGeolocator;
		private readonly ICacheManagerFactory _cacheManagerFactory;

		private ICacheManager _cache;
		public ICacheManager Cache => _cache ?? (_cache = _cacheManagerFactory.CreateCacheManager());

		public AuthorizationFilter(IIpGeolocator ipGeolocator, ICacheManagerFactory cacheManagerFactory)
		{
			_ipGeolocator = ipGeolocator;
			_cacheManagerFactory = cacheManagerFactory;

			AllowMultiple = false;
		}

		public bool AllowMultiple { get; }

		public Task<HttpResponseMessage> ExecuteAuthorizationFilterAsync(
			HttpActionContext actionContext,
			CancellationToken cancellationToken,
			Func<Task<HttpResponseMessage>> continuation)
		{
			if (IsAuthorizated(actionContext))
			{
				return continuation();
			}

			actionContext.Response = actionContext.Request.CreateResponse(
				HttpStatusCode.Forbidden,
				new ErrorResponse(ResponseStatus.Error, "User rate limit exceeded")
			);

			return Task.FromResult(actionContext.Response);
		}

		private bool IsAuthorizated(HttpActionContext actionContext)
		{
			string userIp = GetUserIpAddress(actionContext.Request);

			string ipCountryCode = _ipGeolocator.GetIpCountryCode("178.124.173.22");
			if (!AppConfig.AccessValidCountryCodes.Contains(ipCountryCode))
			{
				actionContext.Response = actionContext.Request.CreateResponse(
					HttpStatusCode.Forbidden,
					new ErrorResponse(ResponseStatus.Error, "Service is not available in your country")
				);

				return false;
			}

			int attemps = _cache.Get<int>(userIp);
			if (attemps > AppConfig.AccessAttemptCount)
			{
				
			}

			return true;
		}

		private string GetUserIpAddress(HttpRequestMessage request)
		{
			const string httpContextKey = "MS_HttpContext";
			if (request.Properties.ContainsKey(httpContextKey))
			{
				var context = request.Properties[httpContextKey] as HttpContextBase;
				if (context != null)
				{
					return context.Request.UserHostAddress;
				}
			}

			const string owinContextKey = "MS_OwinContext";
			if (request.Properties.ContainsKey(owinContextKey))
			{
				var context = request.Properties[owinContextKey] as OwinContext;
				if (context != null)
				{
					return context.Request.RemoteIpAddress;
				}
			}

			return string.Empty;
		}
	}
}