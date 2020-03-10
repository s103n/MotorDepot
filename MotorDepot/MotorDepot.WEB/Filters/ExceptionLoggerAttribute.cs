using MotorDepot.BLL.Interfaces;
using MotorDepot.BLL.Models;
using System;
using System.Web;
using System.Web.Mvc;
using MotorDepot.Shared.Enums;

namespace MotorDepot.WEB.Filters
{
    public class ExceptionLoggerAttribute : FilterAttribute { }

    public class ExceptionLoggerFilter : IExceptionFilter
    {
        private readonly ILoggerService _logger;

        public ExceptionLoggerFilter(ILoggerService logger)
        {
            _logger = logger;
        }

        public void OnException(ExceptionContext filterContext)
        {
            if (new HttpException(null, filterContext.Exception).GetHttpCode() == 500)
            {
                return;
            }

            filterContext.ExceptionHandled = true;
            filterContext.HttpContext.Response.Clear();
            filterContext.HttpContext.Response.StatusCode = 500;

            _logger.Log(new LogEventDto
            {
                Message = filterContext.Exception.Message,
                StackTrace = filterContext.Exception.StackTrace,
                Controller = filterContext.RouteData.Values["controller"].ToString(),
                Action = filterContext.RouteData.Values["action"].ToString(),
                RouteValues = filterContext.RequestContext.HttpContext.Request.Url?.Scheme,
                Ip = filterContext.HttpContext.Request.UserHostAddress,
                LogType = LogType.Exception,
                Time = DateTime.Now
            });
        }
    }
}