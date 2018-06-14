using System;
using System.Linq;
using System.Web.Http;
using Crexi.WeatherForecast.Common.Extensions;
using Crexi.WeatherForecast.Infrastructure;

namespace Crexi.WeatherForecast.Controllers
{
	public class BaseController : ApiController
	{
		#region IoC

		public BaseController(IServiceInterceptor serviceInterceptor)
		{
			serviceInterceptor.ErrorOccured += OnApiErrorOccured;
		}

		#endregion

		#region Error handling

		protected bool IsValid => ModelState.IsValid;

		protected string ErrorMessage { get; set; }

		protected Exception LastError { get; set; }

		private void OnApiErrorOccured(object sender, ServiceErrorEventArgs e)
		{
			if (e.ErrorMessage.HasValue())
			{
				ErrorMessage = e.ErrorMessage;
				ModelState.AddModelError(string.Empty, ErrorMessage);
			}
			else if (e.Error != null)
			{
				HandleException(e.Error);
			}
			else
			{
				e.Handled = false;
				return;
			}

			e.Handled = true;
		}

		protected void HandleException(Exception exception)
		{
			ErrorMessage = exception.Message;
			ModelState.AddModelError(string.Empty, ErrorMessage);

			LastError = exception.InnerException ?? exception;
		}

		protected string GetModelStateErrors()
		{
			return string.Join("<br/>", ModelState.Values.SelectMany(x => x.Errors).Select(x => x.ErrorMessage));
		}

		#endregion
	}
}