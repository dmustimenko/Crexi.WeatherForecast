using System;
using System.Net;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using System.Web.Http.Filters;
using Crexi.WeatherForecast.Models;
using Crexi.WeatherForecast.Shared.Enums;
using Crexi.WeatherForecast.Shared.Exceptions;

namespace Crexi.WeatherForecast.Infrastructure
{
	public class ExceptionFilter : IExceptionFilter
	{
		public bool AllowMultiple { get; }

		public ExceptionFilter()
		{
			AllowMultiple = false;
		}

		public Task ExecuteExceptionFilterAsync(HttpActionExecutedContext actionExecutedContext, CancellationToken cancellationToken)
		{
			var statusCode = HttpStatusCode.InternalServerError;
			string message = "An error has occurred";

			if (actionExecutedContext.Exception is ServiceException)
			{
				
			}

			actionExecutedContext.Response = actionExecutedContext
				.Request
				.CreateResponse(
					statusCode,
					new ErrorResponse(ResponseStatus.Error, message)
				);

			return Task.FromResult<object>(null);
		}
	}
}