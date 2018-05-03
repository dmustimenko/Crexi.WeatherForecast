using System;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using System.Web.Http.Controllers;
using System.Web.Http.Filters;
using Crexi.WeatherForecast.Common.Logger;

namespace Crexi.WeatherForecast.Infrastructure
{
	public class AuthorizationFilter : IAuthorizationFilter
	{
		private readonly ILogger _logger;

		public AuthorizationFilter(
			ILogger logger)
		{
			_logger = logger;
			AllowMultiple = false;
		}

		public bool AllowMultiple { get; }

		public Task<HttpResponseMessage> ExecuteAuthorizationFilterAsync(
			HttpActionContext actionContext,
			CancellationToken cancellationToken,
			Func<Task<HttpResponseMessage>> continuation)
		{
			//AuthenticateRequest(actionContext);
			//if (actionContext.Response == null)
			//{
			//	AuthorizeRequest(actionContext);
			//}

			//if (actionContext.Response == null)
			//{
			//	return continuation();
			//}

			return Task.FromResult(actionContext.Response);
		}
	}
}