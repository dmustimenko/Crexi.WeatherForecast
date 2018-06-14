using System;
using Crexi.WeatherForecast.Common.Logger;
using Crexi.WeatherForecast.Shared.Exceptions;
using LightInject.Interception;

namespace Crexi.WeatherForecast.Infrastructure
{
	public class ServiceInterceptor : IServiceInterceptor
	{
		private readonly ILogger _logger;

		public event EventHandler<ServiceErrorEventArgs> ErrorOccured = delegate { };

		public ServiceInterceptor(ILogger logger)
		{
			_logger = logger;
		}

		public object Invoke(IInvocationInfo invocationInfo)
		{
			try
			{
				return invocationInfo.Proceed();
			}
			catch (Exception exception)
			{
				var serviceException = exception as ServiceException;
				var eventArgs = serviceException == null
					? new ServiceErrorEventArgs { Error = exception, ErrorMessage = "Service error has occurred" }
					: new ServiceErrorEventArgs { Error = exception };

				ErrorOccured(this, eventArgs);

				if (!eventArgs.Handled)
				{
					throw;
				}

				_logger.Error(exception);

				var returnType = invocationInfo.Method.ReturnType;
				if (returnType.IsValueType && returnType != typeof(void))
				{
					return Activator.CreateInstance(invocationInfo.Method.ReturnType);
				}

				return null;
			}
		}
	}
}