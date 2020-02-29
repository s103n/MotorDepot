using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using MotorDepot.BLL.Interfaces;
using MotorDepot.WEB.Models;

namespace MotorDepot.WEB.Controllers
{
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

        public async Task<ActionResult> All()
        {
            return null;
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
    }
}