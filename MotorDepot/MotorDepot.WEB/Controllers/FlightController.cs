using System;
using Microsoft.AspNet.Identity;
using MotorDepot.BLL.Infrastructure.Enums;
using MotorDepot.BLL.Interfaces;
using MotorDepot.WEB.Infrastructure.Mappers;
using MotorDepot.WEB.Models;
using MotorDepot.WEB.Models.Enums;
using System.Collections.Generic;
using System.Net;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace MotorDepot.WEB.Controllers
{
    [Authorize]
    public class FlightController : Controller
    {
        private readonly IFlightService _flightService;
        private readonly IDriverService _driverService;
        private readonly IUserService _userService;
        private readonly IDispatcherService _dispatcherService;
        private readonly IFlightRequestService _flightRequestService;
        private readonly List<AlertViewModel> _alerts = new List<AlertViewModel>();

        public FlightController(IFlightService flightService,
            IDriverService driverService,
            IUserService userService,
            IDispatcherService dispatcherService,
            IFlightRequestService flightRequestService)
        {
            _flightService = flightService;
            _driverService = driverService;
            _userService = userService;
            _dispatcherService = dispatcherService;
            _flightRequestService = flightRequestService;
        }

        public ActionResult Index()
        {
            return new HttpNotFoundResult();
        }

        [Authorize(Roles = "driver, dispatcher, admin")]
        public async Task<ActionResult> All()
        {
            var flightOperation = await _flightService.GetAllAsync();

            return View(flightOperation.Value.ToFlightVm());
        }

        [Authorize(Roles = "dispatcher, admin")]
        public async Task<ActionResult> Requests()
        {
            var flightOperation = await _flightRequestService.GetFlightRequestsAsync();

            if (flightOperation.Code == HttpStatusCode.OK)
            {
                return View(flightOperation.Value.ToDisplayViewModels());
            }

            return new HttpNotFoundResult();
        }

        [Authorize(Roles = "dispatcher, admin")]
        public async Task<ActionResult> ConfirmRequest(int? requestId)
        {
            var requestOperation = await _flightRequestService.GetRequestByIdAsync(requestId);

            if (requestOperation.Code == HttpStatusCode.OK)
            {
                return View(requestOperation.Value.ToDetailsViewModel());
            }

            return new HttpNotFoundResult();
        }

        [HttpPost]
        [Authorize(Roles = "dispatcher, admin")]
        public async Task<ActionResult> ConfirmRequest(int? requestId, bool isAccepted)
        {
            var status = isAccepted ? FlightRequestStatus.Accepted : FlightRequestStatus.Canceled;

            var requestOperation = await _flightRequestService.SetRequestStatus(
                requestId,
                User.Identity.GetUserId(),
                status);

            if (requestOperation.Code == HttpStatusCode.OK)
            {
                _alerts.Clear();
                _alerts.Add(new AlertViewModel(
                    $"Request #{requestId} was {status.ToString().ToLower()}",
                    AlertType.Success));

                return View("~/Views/Home/Index.cshtml", _alerts);
            }

            return new HttpStatusCodeResult(requestOperation.Code);
        }

        [HttpPost]
        public async Task<ActionResult> Edit(FlightViewModel model)
        {
            return null;
        }

        public async Task<ActionResult> Display(int? id)
        {
            return null;
        }

        [Authorize(Roles = "driver")]
        public async Task<ActionResult> RequestFor(int? flightId)
        {
            if (flightId == null || await _flightService.GetByIdAsync(flightId) == null)
                return new HttpNotFoundResult();

            return View(new FlightRequestCreateViewModel { RequestedFlightId = (int)flightId });
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> RequestFor(FlightRequestCreateViewModel model)
        {
            var driverOperation = await _driverService.GetDriverById(User.Identity.GetUserId());
            var requestedFlightOperation = await _flightService.GetByIdAsync(model.RequestedFlightId);

            model.Status = FlightRequestStatus.InQueue;

            if (driverOperation.Code == HttpStatusCode.OK
                && requestedFlightOperation.Code == HttpStatusCode.OK)
            {
                var status = await _driverService.SendFlightRequest(model.ToFlightRequestDto(
                    driverOperation.Value.ToDriverDto(User.Identity.GetUserId()),
                    null,
                    requestedFlightOperation.Value));

                if (status.Code == HttpStatusCode.OK)
                {
                    _alerts.Clear();
                    _alerts.Add(new AlertViewModel(status.Message, AlertType.Success));

                    return View("~/Views/Home/Index.cshtml", _alerts);
                }

                ModelState.AddModelError("", status.Message);
            }

            ModelState.AddModelError("", driverOperation.Message);
            ModelState.AddModelError("", requestedFlightOperation.Message);

            return View(model);
        }
    }
}