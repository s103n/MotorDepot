using Microsoft.AspNet.Identity;
using MotorDepot.BLL.Interfaces;
using MotorDepot.WEB.Infrastructure.Mappers;
using MotorDepot.WEB.Models;
using MotorDepot.WEB.Models.Enums;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Web.Mvc;
using MotorDepot.Shared.Enums;
using MotorDepot.WEB.Infrastructure;

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
            var flightOperation = await _flightRequestService.GetFlightRequestsAsync(FlightRequestStatus.InQueue);

            if (flightOperation.Success)
            {
                return View(flightOperation.Value.ToDisplayViewModels());
            }

            return new HttpOperationStatusResult(flightOperation);
        }

        [Authorize(Roles = "dispatcher, admin")]
        public async Task<ActionResult> ConfirmRequest(int? requestId)
        {
            var requestOperation = await _flightRequestService.GetRequestByIdAsync(requestId);

            if (requestOperation.Success)
            {
                var autoOperation = await _autoService.GetAutosByTypeAsync(requestOperation.Value.AutoType);

                if (autoOperation.Success)
                {
                    var acceptViewModel = new FlightRequestAcceptViewModel
                    {
                        Request = requestOperation.Value.ToDetailsViewModel(),
                        Auto = autoOperation.Value.ToSetViewModels()
                    };

                    return View(acceptViewModel);
                }

                return new HttpOperationStatusResult(autoOperation);
            }

            return new HttpOperationStatusResult(requestOperation);
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

            return new HttpOperationStatusResult(flightOperation, requestOperation);
        }

        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create(FlightCreateViewModel model)
        {
            var createOperation = await _flightService.CreateAsync(model.ToDto());

            if (createOperation.Success)
            {
                _alerts.Clear();
                _alerts.Add(new AlertViewModel("Flight was created successful", AlertType.Success));

                return View("~/Views/Home/Index.cshtml", _alerts);
            }

            return new HttpOperationStatusResult(createOperation);
        }

        public async Task<ActionResult> Edit(int? flightId)
        {
            var operation = await _flightService.GetByIdAsync(flightId);

            if (operation.Success)
            {
                //изменять если статус Free
                //заблокировать кнопку при добавление машины)0выф
                var operationStatus = _flightService.GetFlightStatuses();
                ViewBag.FlightStatuses = new SelectList(operationStatus.Value, "Id", "Name");

                return View(operation.Value.ToEditViewModel());
            }

            return new HttpOperationStatusResult(operation);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit(FlightEditViewModel model)
        {
            var operation = await _flightService.EditAsync(model.ToDto());

            if (operation.Success)
            {
                _alerts.Clear();
                _alerts.Add(new AlertViewModel("Flight was updated", AlertType.Success));

                return View("~/Views/Home/Index.cshtml", _alerts);
            }

            return new HttpOperationStatusResult(operation);
        }

        public async Task<ActionResult> Details(int? id)
        {
            return null;
        }

        [Authorize(Roles = "driver")]
        public async Task<ActionResult> RequestFor(int? flightId)
        {
            var operation = await _flightService.GetByIdAsync(flightId);
            var operationAuto = _autoService.GetAutoTypes();

            ViewBag.AutoTypes = new SelectList(operationAuto.Value, "Id", "Name");

            if (operation.Success)
            {
                return View(new FlightRequestCreateViewModel {RequestedFlightId = (int) flightId});
            }

            return new HttpOperationStatusResult(operation, operationAuto);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> RequestFor(FlightRequestCreateViewModel model)
        {
            var driverOperation = await _driverService.GetDriverById(User.Identity.GetUserId());
            var requestedFlightOperation = await _flightService.GetByIdAsync(model.RequestedFlightId);

            model.DriverId = User.Identity.GetUserId();
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

                return new HttpOperationStatusResult(operation);
            }

            return new HttpOperationStatusResult(driverOperation, requestedFlightOperation);
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