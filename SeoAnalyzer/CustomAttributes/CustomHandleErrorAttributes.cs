using CoreLibrary;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace SeoAnalyzer.CustomAttributes
{
	public class CustomHandleErrorAttribute : HandleErrorAttribute
	{
		public override void OnException(ExceptionContext filterContext)
		{
			Exception exception = HttpContext.Current.Server.GetLastError();
			HttpContext.Current.Response.Clear();

			HttpException httpException = new HttpException(null, filterContext.Exception);

			if (httpException != null)
			{
				string action;

				switch (httpException.GetHttpCode())
				{
					case 404:
						// page not found
						action = "HttpError404";
						break;
					case 500:
						// server error
						action = "HttpError500";
						break;
					default:
						action = "HttpError500";
						break;
				}

				Log.AddErrorLog(httpException);

				// clear error on server
				HttpContext.Current.Server.ClearError();
				HttpContext.Current.Response.Redirect($"Error/{action}");

			}
		}
	}
}
