using System;
using System.Net;
using Microsoft.AspNet.Identity;
using MotorDepot.BLL.Interfaces;
using MotorDepot.Shared.Enums;
using MotorDepot.WEB.Infrastructure;
using MotorDepot.WEB.Infrastructure.Mappers;
using MotorDepot.WEB.Models;
using MotorDepot.WEB.Models.Enums;
using MotorDepot.WEB.Models.Flight;
using MotorDepot.WEB.Models.FlightRequest;
using System.Threading.Tasks;
using System.Web.Mvc;
using MotorDepot.BLL.BusinessModels;

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
        public async Task<ActionResult> All(string sortProperty = null) //todo asc
        {
            if(User.IsInRole("driver"))
            {
                var flights= await _flightService.GetAllAsync(onlyFree: true);

                return View(flights.Value.ToDisplayViewModel());
            }

            var flightOperation = await _flightService.GetAllAsync();

            if (sortProperty != null)
            {
                try
                {
                    var items = ReflectionSort<FlightViewModel>
                        .Sort(flightOperation.Value.ToDisplayViewModel(), sortProperty);

                    return View(items);
                }
                catch (Exception ex)
                {
                    return new HttpStatusCodeResult(HttpStatusCode.BadRequest, ex.Message);
                }
            }

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
            var createOperation = await _flightService.CreateAsync(model.ToDto());

            if (createOperation.Success)
            {
                Session["Create"] = new AlertViewModel(createOperation.Message, AlertType.Success);

                return RedirectToAction("Index", "Home");
            }

            return new HttpOperationStatusResult(createOperation);
        }

        public async Task<ActionResult> Edit(int? flightId)
        {
            var operation = await _flightService.GetByIdAsync(flightId);

            if (operation.Success)
            {
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
                Session["Edit"] = new AlertViewModel(operation.Message, AlertType.Success);

                return RedirectToAction("Index", "Home");
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
                return View(new FlightRequestCreateViewModel { RequestedFlightId = (int)flightId });
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
                    Session["RequestForFlight"] = new AlertViewModel(operation.Message, AlertType.Success);

                    return RedirectToAction("Index", "Home");
                }

                return new HttpOperationStatusResult(operation);
            }

            return new HttpOperationStatusResult(driverOperation, requestedFlightOperation);
        }

        [HttpGet] //todo delete confirm
        public async Task<ActionResult> DeleteDriver(int? flightId)
        {
            var deleteOperation = await _flightService.DeleteDriverAndAuto(flightId);

            if (deleteOperation.Success)
            {
                Session["Delete"] = new AlertViewModel(deleteOperation.Message, AlertType.Success);

                return RedirectToAction("All");
            }

            if (deleteOperation.Code == HttpStatusCode.BadRequest) // need to check for error
            {
                ModelState.AddModelError("", deleteOperation.Message);

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