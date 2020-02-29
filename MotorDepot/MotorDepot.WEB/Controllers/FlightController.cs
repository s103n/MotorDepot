using MotorDepot.BLL.Interfaces;
using MotorDepot.WEB.Models;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace MotorDepot.WEB.Controllers
{
    [Authorize(Roles = "admin, dispatcher")]
    public class FlightController : Controller
    {
        private readonly IFlightService _flightService;

        public FlightController(IFlightService flightService)
        {
            _flightService = flightService;
        }

        public ActionResult Index()
        {
            return View();
        }

        public ActionResult All()
        {
            return View(_flightService.GetAll());
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
    }
}