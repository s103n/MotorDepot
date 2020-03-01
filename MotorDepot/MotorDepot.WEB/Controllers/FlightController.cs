using MotorDepot.BLL.Interfaces;
using MotorDepot.WEB.Models;
using System.Threading.Tasks;
using System.Web.Mvc;
using Microsoft.AspNet.Identity;
using MotorDepot.BLL.Infrastructure.Enums;
using MotorDepot.WEB.Infrastructure.Mappers;

namespace MotorDepot.WEB.Controllers
{
    [Authorize]
    public class FlightController : Controller
    {
        private readonly IFlightService _flightService;
        private readonly IDriverService _driverService;
        private readonly IUserService _userService;

        public FlightController(IFlightService flightService, 
            IDriverService driverService,
            IUserService userService)
        {
            _flightService = flightService;
            _driverService = driverService;
            _userService = userService;
        }

        public ActionResult Index()
        {
            return View();
        }

        [Authorize(Roles = "driver, dispatcher, admin")]
        public ActionResult All()
        {
            return View(_flightService.GetAll().ToFlightVm());
        }

        public async Task<ActionResult> Requests()
        {
            return null;
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
        public ActionResult RequestFor(int flightId)
        {
            return View(new FlightRequestViewModel{ RequestedFlightId = flightId});
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> RequestFor(FlightRequestViewModel model)
        {
            var driver = await _driverService.GetDriverById(User.Identity.GetUserId());
            var requestedFlight = await _flightService.GetByIdAsync(model.RequestedFlightId);

            model.Status = FlightRequestStatus.InQueue;

            await _driverService.SendFlightRequest(model.ToFlightRequestDto(driver, null, requestedFlight));

            return RedirectToAction("Index", "Home");
        } 
    }
}