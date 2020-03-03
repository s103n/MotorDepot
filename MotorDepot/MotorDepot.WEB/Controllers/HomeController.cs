using MotorDepot.WEB.Models;
using System.Collections.Generic;
using System.Web.Mvc;

namespace MotorDepot.WEB.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index(IEnumerable<AlertViewModel> alerts)
        {
            return View(alerts ?? new List<AlertViewModel>());
        }
    }
}