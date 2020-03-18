using Microsoft.AspNet.Identity;
using MotorDepot.BLL.Interfaces;
using MotorDepot.Shared.Enums;
using MotorDepot.WEB.Filters;
using MotorDepot.WEB.Infrastructure;
using MotorDepot.WEB.Infrastructure.Mappers;
using MotorDepot.WEB.Models;
using MotorDepot.WEB.Models.Enums;
using MotorDepot.WEB.Models.Flight;
using MotorDepot.WEB.Models.FlightRequest;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace MotorDepot.WEB.Controllers
{
    [Authorize]
    [ExceptionLogger]
    [ActionLogger]
    public class FlightController : Controller
    {
        private readonly IFlightService _flightService;
        private readonly IDriverService _driverService;
        private readonly IUserService _userService;
        private readonly IDispatcherService _dispatcherService;
        private readonly IFlightRequestService _flightRequestService;
        private readonly IAutoService _autoService;

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
        public async Task<ActionResult> All(FlightStatus? status = null)
        {
            if (User.IsInRole("driver"))
            {
                var flightItems = (await _flightService.GetFlightsAsync(FlightStatus.Free)).ToDisplayViewModel();

                return View(flightItems);
            }

            var flightOperation = await _flightService.GetFlightsAsync(status);

            ViewBag.StatusTitle = status == null ? "All" : status.ToString();

            return View(flightOperation.ToDisplayViewModel());
        }

        [Authorize(Roles = "dispatcher, admin")]
        public async Task<ActionResult> Requests(FlightRequestStatus? status)
        {
            var flightOperation = await _flightRequestService.GetFlightRequestsAsync(status);
            ViewBag.StatusTitle = status == null ? "All" : status.ToString();

            return View(flightOperation.ToDisplayViewModels());
        }

        [Authorize(Roles = "dispatcher, admin")]
        public async Task<ActionResult> ConfirmRequest(int? requestId)
        {
            var requestOperation = await _flightRequestService.GetRequestByIdAsync(requestId);

            if (requestOperation.Success)
            {
                var autos = await _autoService.GetAutosByTypeAsync(requestOperation.Value.AutoType);

                var acceptViewModel = new FlightRequestAcceptViewModel
                {
                    Request = requestOperation.Value.ToDetailsViewModel(),
                    Auto = autos.ToSetViewModels()
                };

                return View(acceptViewModel);
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

            var requestOperation = await _flightRequestService.ConfirmRequestAsync(
                requestId,
                User.Identity.GetUserId(),
                status);

            var flightOperation = await _flightService.SetDriverWithAuto(flightId, autoId, driverEmail);

            if (requestOperation.Success && flightOperation.Success)
            {
                Session["Edit"] = new AlertViewModel(requestOperation.Message, AlertType.Success);
                Session["FlightStatus"] = new AlertViewModel(flightOperation.Message, AlertType.Success);

                return RedirectToAction("Index", "Home");
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
            var createOperation = await _flightService.CreateFlightAsync(model.ToDto());

            if (createOperation.Success)
            {
                Session["Create"] = new AlertViewModel(createOperation.Message, AlertType.Success);

                return RedirectToAction("Index", "Home");
            }

            return new HttpOperationStatusResult(createOperation);
        }

        public async Task<ActionResult> Edit(int? id)
        {
            var operation = await _flightService.GetFlightAsync(id);

            if (operation.Success)
            {
                if (operation.Value.Status != FlightStatus.Free)
                    return HttpNotFound();

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
            var operation = await _flightService.EditFlightAsync(model.ToDto());

            if (operation.Success)
            {
                Session["Edit"] = new AlertViewModel(operation.Message, AlertType.Success);

                return RedirectToAction("Index", "Home");
            }

            return new HttpOperationStatusResult(operation);
        }

        public async Task<ActionResult> Details(int? id)
        {
            var flightOperation = await _flightService.GetFlightAsync(id);

            if (flightOperation.Success)
            {
                return View(flightOperation.Value.ToDetailsViewModel());
            }

            return new HttpOperationStatusResult(flightOperation);
        }

        [Authorize(Roles = "driver")]
        public async Task<ActionResult> RequestFor(int? id)
        {
            var operation = await _flightService.GetFlightAsync(id);
            var autoTypes = _autoService.GetAutoTypes();

            ViewBag.AutoTypes = new SelectList(autoTypes, "Id", "Name");

            if (operation.Success)
            {
                if (id != null) return View(new FlightRequestCreateViewModel {RequestedFlightId = (int) id});
            }

            return new HttpOperationStatusResult(operation);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> RequestFor(FlightRequestCreateViewModel model)
        {
            var driverOperation = await _driverService.GetDriverById(User.Identity.GetUserId());
            var requestedFlightOperation = await _flightService.GetFlightAsync(model.RequestedFlightId);

            model.DriverId = User.Identity.GetUserId();
            model.Status = FlightRequestStatus.InQueue;

            if (driverOperation.Success && requestedFlightOperation.Success)
            {
                var operation = await _driverService.SendFlightRequest(model.ToDto());

                if (operation.Success)
                {
                    Session["RequestForFlight"] = new AlertViewModel(operation.Message, AlertType.Success);

                    return RedirectToAction("Index", "Home");
                }

                return new HttpOperationStatusResult(operation);
            }

            return new HttpOperationStatusResult(driverOperation, requestedFlightOperation);
        }

        [HttpGet]
        public async Task<ActionResult> DeleteDriver(int? id)
        {
            var deleteOperation = await _flightService.DeleteDriverAndAuto(id);

            if (deleteOperation.Success)
            {
                Session["Delete"] = new AlertViewModel(deleteOperation.Message, AlertType.Success);

                return RedirectToAction("All");
            }

            return new HttpOperationStatusResult(deleteOperation);
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