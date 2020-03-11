using MotorDepot.BLL.Interfaces;
using MotorDepot.WEB.Infrastructure;
using MotorDepot.WEB.Infrastructure.Mappers;
using MotorDepot.WEB.Models;
using MotorDepot.WEB.Models.Auto;
using MotorDepot.WEB.Models.Enums;
using System.Threading.Tasks;
using System.Web.Mvc;
using MotorDepot.Shared.Enums;
using MotorDepot.WEB.Filters;

namespace MotorDepot.WEB.Controllers
{
    [Authorize(Roles = "admin")]
    [ExceptionLogger]
    [ActionLogger]
    public class AdminController : Controller
    {
        private readonly IDispatcherService _dispatcherService;
        private readonly IDriverService _driverService;
        private readonly ILoggerService _loggerService;

        public AdminController(IDispatcherService dispatcherService, 
            IDriverService driverService,
            ILoggerService loggerService)
        {
            _loggerService = loggerService;
            _driverService = driverService;
            _dispatcherService = dispatcherService;
        }

        public ActionResult AddDispatcher()
        {
            return View("AddUser");
        }

        [HttpPost]
        public async Task<ActionResult> AddDispatcher(UserRegisterViewModel model)
        {
            if (ModelState.IsValid)
            {
                var operation = await _dispatcherService.CreateDispatcher(model.ToUserDto("dispatcher"));

                if (operation.Success)
                {
                    Session["Create"] = new AlertViewModel(operation.Message, AlertType.Success);

                    return RedirectToAction("Index", "Home");
                }

                return new HttpOperationStatusResult(operation);
            }

            return View("AddUser", model);
        }

        public ActionResult Dispatchers()
        {
            var dispatchers = _dispatcherService.GetDispatchers();

            return View(dispatchers);
        }

        public ActionResult AddDriver()
        {
            return View("AddUser");
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> AddDriver(UserRegisterViewModel model)
        {
            if (ModelState.IsValid)
            {
                var operation = await _driverService.CreateDriver(model.ToUserDto("driver"));

                if (operation.Success)
                {
                    Session["Create"] = new AlertViewModel(operation.Message, AlertType.Success);

                    return RedirectToAction("Index", "Home");
                }

                return new HttpOperationStatusResult(operation);
            }

            return View("AddUser", model);
        }

        public ActionResult Log(LogType logType = LogType.Action)
        {
            var logs = _loggerService.GetLogs(logType).ToViewModel();

            ViewBag.LogTypes = new SelectList(_loggerService.GetLogTypes(), "Id", "Name");

            return View(logs);
        }

        public ActionResult LogAjax(LogType logType)
        {
            var logs = _loggerService.GetLogs(logType).ToViewModel();

            return PartialView("LogTable", logs);
        }

        public ActionResult LogDetails(int? logId)
        {
            var log = _loggerService.GetLogById(logId);

            if (log.Success)
            {
                switch (log.Value.LogType)
                {
                    case LogType.Action:
                        return View("LogAction", log.Value.ToDetailsAction()); //to action view model
                    case LogType.Exception:
                        return View("LogException", log.Value.ToDetailsException()); //to exception view model
                    case LogType.Warning:
                        return View(log.Value); //to warning view model
                    default:
                        return HttpNotFound();
                }
            }

            return new HttpOperationStatusResult(log);
        }

        protected override void Dispose(bool disposing)
        {
            _dispatcherService.Dispose();
            _driverService.Dispose();
            base.Dispose(disposing);
        }
    }
}