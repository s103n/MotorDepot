using System.Linq;
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
        private readonly IFlightService _flightService;
        private readonly IFlightRequestService _flightRequestService;

        public AdminController(IDispatcherService dispatcherService, 
            IDriverService driverService,
            ILoggerService loggerService,
            IFlightService flightService,
            IFlightRequestService flightRequestService)
        {
            _flightRequestService = flightRequestService;
            _flightService = flightService;
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

        public async Task<ActionResult> Dispatchers()
        {
            var dispatchers = await _dispatcherService.GetDispatchers();

            return View(dispatchers.ToViewModelDispatcher());
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
                    default:
                        return HttpNotFound();
                }
            }

            return new HttpOperationStatusResult(log);
        }

        public async Task<ActionResult> Drivers()
        {
            var drivers = await _driverService.GetDriversAsync();

            return View(drivers.ToViewModelDriver());
        }

        public async Task<ActionResult> DriverDetails(string id)
        {
            var driver = await _driverService.GetDriverById(id);
            var flights = (await _flightService.GetFlightsAsync(null))
                .Where(f => f.Driver != null && f.Driver.Id == id);

            if (driver.Success)
            {
                return View(driver.Value.ToDriverDetailsViewModel(flights.ToDisplayViewModel()));
            }

            return new HttpOperationStatusResult(driver);
        }

        public async Task<ActionResult> DispatcherDetails(string id)
        {
            var dispatcher = await _dispatcherService.GetDispatcherAsync(id);
            var flights = (await _flightService.GetFlightsAsync(null))
                .Where(f => f.DispatcherCreator.Id == id).ToList();
            var flightRequests = (await _flightRequestService.GetFlightRequestsAsync())
                .Where(fr => fr.Dispatcher != null && fr.Dispatcher.Id == id).ToList();

            if (dispatcher.Success)
            {
                return View(dispatcher.Value.ToDispatcherDetailsViewModel(
                    flights.ToDisplayViewModel(),
                    flightRequests.ToDisplayViewModels()));
            }

            return new HttpOperationStatusResult(dispatcher);
        }

        protected override void Dispose(bool disposing)
        {
            _dispatcherService.Dispose();
            _driverService.Dispose();
            base.Dispose(disposing);
        }
    }
}