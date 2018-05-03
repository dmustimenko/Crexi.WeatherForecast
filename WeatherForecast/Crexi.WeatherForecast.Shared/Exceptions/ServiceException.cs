using System;

namespace Crexi.WeatherForecast.Shared.Exceptions
{
	public class ServiceException : Exception
	{
		public ServiceException(string message)
			: base(message) { }

		public ServiceException(string format, params object[] args)
			: base(string.Format(format, args)) { }

		public ServiceException(string message, Exception innerException)
			: base(message, innerException) { }

		public ServiceException(string format, Exception innerException, params object[] args)
			: base(string.Format(format, args), innerException) { }
	}
}
