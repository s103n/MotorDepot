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
        private readonly IAutoService _autoService;
        private readonly List<AlertViewModel> _alerts = new List<AlertViewModel>();

        public FlightController(IFlightService flightService,
            IDriverService driverService,
            IUserService userService,
            IDispatcherService dispatcherService,
            IFlightRequestService flightRequestService,
            IAutoService autoService)
        {
            _flightService = flightService;
            _driverService = driverService;
            _userService = userService;
            _dispatcherService = dispatcherService;
            _flightRequestService = flightRequestService;
            _autoService = autoService;
        }

        public ActionResult Index()
        {
            return new HttpNotFoundResult();
        }

        [Authorize(Roles = "driver, dispatcher, admin")]
        public async Task<ActionResult> All()
        {
            var flightOperation = await _flightService.GetAllAsync();

            return View(flightOperation.Value.ToDisplayViewModel());
        }

        [Authorize(Roles = "dispatcher, admin")]
        public async Task<ActionResult> Requests()
        {
            var flightOperation = await _flightRequestService.GetFlightRequestsAsync();

            if (flightOperation.Success)
            {
                return View(flightOperation.Value.ToDisplayViewModels());
            }

            return new HttpNotFoundResult();
        }

        [Authorize(Roles = "dispatcher, admin")]
        public async Task<ActionResult> ConfirmRequest(int? requestId)
        {
            var requestOperation = await _flightRequestService.GetRequestByIdAsync(requestId);
            var autoOperation = await _autoService.GetAutosByTypeAsync(requestOperation.Value.AutoType);

            if (requestOperation.Success && autoOperation.Success)
            {
                var acceptViewModel = new FlightRequestAcceptViewModel
                {
                    Request = requestOperation.Value.ToDetailsViewModel(),
                    Auto = autoOperation.Value.ToSetViewModels()
                };

                return View(acceptViewModel);
            }

            if (!requestOperation.Success)
                return new HttpStatusCodeResult(requestOperation.Code, requestOperation.Message);

            return new HttpStatusCodeResult(autoOperation.Code, autoOperation.Message);
        }

        [HttpPost]
        [Authorize(Roles = "dispatcher, admin")]
        public async Task<ActionResult> ConfirmRequest(
            int requestId,
            int flightId,
            int? autoId,
            string driverEmail,
            bool isAccepted)
        {
            var status = isAccepted ? FlightRequestStatus.Accepted : FlightRequestStatus.Canceled;

            var requestOperation = await _flightRequestService.ConfirmRequest(
                requestId,
                User.Identity.GetUserId(),
                status);

            var flightOperation = await _flightService.SetDriverWithAuto(flightId, (int)autoId, driverEmail);

            if (requestOperation.Success && flightOperation.Success)
            {
                _alerts.Clear();
                _alerts.Add(new AlertViewModel(
                    $"Request #{requestId} was {status.ToString().ToLower()}",
                    AlertType.Success));
                _alerts.Add(new AlertViewModel(
                    $"Flight #{flightId} was occupied by driver {driverEmail} with auto #{autoId}",
                    AlertType.Success));

                return View("~/Views/Home/Index.cshtml", _alerts);
            }

            if(!requestOperation.Success)
                return new HttpStatusCodeResult(requestOperation.Code, requestOperation.Message);

            return new HttpStatusCodeResult(flightOperation.Code, flightOperation.Message);
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
            var operation = await _flightService.GetByIdAsync(flightId);
            //add autoType in viewBag

            if (operation.Success)
            {
                return View(new FlightRequestCreateViewModel {RequestedFlightId = (int) flightId});
            }

            return new HttpStatusCodeResult(operation.Code, operation.Message);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> RequestFor(FlightRequestCreateViewModel model)
        {
            var driverOperation = await _driverService.GetDriverById(User.Identity.GetUserId());
            var requestedFlightOperation = await _flightService.GetByIdAsync(model.RequestedFlightId);

            model.Status = FlightRequestStatus.InQueue;

            if (driverOperation.Success && requestedFlightOperation.Success)
            {
                var operation = await _driverService.SendFlightRequest(model.ToDto());

                if (operation.Success)
                {
                    _alerts.Clear();
                    _alerts.Add(new AlertViewModel(operation.Message, AlertType.Success));

                    return View("~/Views/Home/Index.cshtml", _alerts);
                }

                return new HttpStatusCodeResult(operation.Code, operation.Message);
            }

            if(!driverOperation.Success)
                return new HttpStatusCodeResult(driverOperation.Code, driverOperation.Message);

            return new HttpStatusCodeResult(requestedFlightOperation.Code, requestedFlightOperation.Message);
        }

        protected override void Dispose(bool disposing)
        {
            _autoService.Dispose();
            _dispatcherService.Dispose();
            _driverService.Dispose();
            _flightService.Dispose();
            _flightRequestService.Dispose();
            _userService.Dispose();
            base.Dispose(disposing);
        }
    }
}