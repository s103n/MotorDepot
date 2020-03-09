using MotorDepot.BLL.Interfaces;
using MotorDepot.BLL.Models;
using System;
using System.Web.Mvc;

namespace MotorDepot.WEB.Filters
{
    public class ExceptionLoggerAttribute : FilterAttribute, IExceptionFilter
    {
        private readonly ILoggerService _logger;
        public ExceptionLoggerAttribute(ILoggerService logger)
        {
            _logger = logger;
        }

        public async void OnException(ExceptionContext filterContext)
        {
            await _logger.Log(new ExceptionDto()
            {
                Message = filterContext.Exception.Message,
                StackTrace = filterContext.Exception.StackTrace,
                Controller = filterContext.RouteData.Values["controller"].ToString(),
                Action = filterContext.RouteData.Values["action"].ToString(),
                Time = DateTime.Now
            });

            filterContext.ExceptionHandled = true;
        }
    }
}