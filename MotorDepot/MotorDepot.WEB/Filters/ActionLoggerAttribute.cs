using MotorDepot.BLL.Interfaces;
using System;
using System.Web.Mvc;
using MotorDepot.BLL.Models;
using MotorDepot.Shared.Enums;

namespace MotorDepot.WEB.Filters
{
    public class ActionLoggerAttribute : FilterAttribute
    {
    }

    public class ActionLoggerFilter : IActionFilter
    {
        private readonly ILoggerService _logger;

        public ActionLoggerFilter(ILoggerService logger)
        {
            _logger = logger;
        }

        public void OnActionExecuting(ActionExecutingContext filterContext)
        {
            _logger.Log(new LogEventDto
            {
                RouteValues = filterContext.HttpContext.Request.RawUrl,
                Action = filterContext.RouteData.Values["action"].ToString(),
                Controller = filterContext.RouteData.Values["controller"].ToString(),
                Message = "",
                StackTrace = "",
                Ip = filterContext.HttpContext.Request.UserHostAddress,
                Time = DateTime.Now,
                LogType = LogType.Action
            });
        }

        public void OnActionExecuted(ActionExecutedContext filterContext)
        {
        }
    }
}