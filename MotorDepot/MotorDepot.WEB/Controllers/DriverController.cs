using System.Threading.Tasks;
using System.Web.Mvc;
using Microsoft.AspNet.Identity;
using MotorDepot.BLL.Interfaces;
using MotorDepot.Shared.Enums;
using MotorDepot.WEB.Filters;
using MotorDepot.WEB.Infrastructure;
using MotorDepot.WEB.Infrastructure.Mappers;
using MotorDepot.WEB.Models;
using MotorDepot.WEB.Models.Enums;
using MotorDepot.WEB.Models.Flight;

namespace MotorDepot.WEB.Controllers
{
    [Authorize(Roles = "driver")]
    [ExceptionLogger]
    [ActionLogger]
    public class DriverController : Controller
    {
        private readonly IFlightService _flightService;
        private readonly IDriverService _driverService;
        private readonly IAutoService _autoService;

        public DriverController(IFlightService flightService,
            IDriverService driverService,
            IAutoService autoService)
        {
            _flightService = flightService;
            _driverService = driverService;
            _autoService = autoService;
        }

        public async Task<ActionResult> MyFlights()
        {
            var driverId = User.Identity.GetUserId();
            var flightsOperation = await _driverService.GetFlightsByDriver(driverId);
            var currentFlightOperation = await _driverService.CurrentFlight(driverId);

            if (flightsOperation.Success && currentFlightOperation.Success)
            {
                var driverFlightVm = new DriverFlightViewModel
                {
                    Flights = flightsOperation.Value.ToDisplayViewModel()
                };

                if (currentFlightOperation.Value != null)
                {
                    driverFlightVm.CurrentFlight = currentFlightOperation.Value.ToDisplayViewModel();

                    ViewBag.AutoStatuses = new SelectList(_autoService.GetAutoStatuses(), "Id", "Name");
                }

                return View(driverFlightVm);
            }

            return new HttpOperationStatusResult(currentFlightOperation, flightsOperation);
        }

        [HttpPost]//todo refact
        public async Task<ActionResult> ChangeStatus(int? flightId, int status, int? autoStatus, int? autoId)
        {
            if (autoId != null && autoStatus != null)
            {
                await _autoService.SetStatus((AutoStatus)autoStatus, (int)autoId);
            }

            var flightOperation = await _flightService.SetStatus((FlightStatus)(++status), flightId);

            if (flightOperation.Success)
            {
                Session["Update"] = new AlertViewModel($"{flightOperation.Message}", AlertType.Success);

                return RedirectToAction("MyFlights");
            }

            return new HttpOperationStatusResult(flightOperation);
        }

        [HttpPost]
        public async Task<ActionResult> CancelFlight(int? flightId)
        {
            var flightOperation = await _flightService.SetStatus(FlightStatus.Free, flightId);

            if (flightOperation.Success)
            {
                Session["Update"] = new AlertViewModel(flightOperation.Message, AlertType.Success);

                return RedirectToAction("MyFlights");
            }

            return new HttpOperationStatusResult(flightOperation);
        }
    }
}