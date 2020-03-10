using MotorDepot.WEB.Infrastructure;
using System.Web.Mvc;
using MotorDepot.WEB.Filters;

namespace MotorDepot.WEB.Controllers
{
    [Authorize]
    [ExceptionLogger]
    [ActionLogger]
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public void SessionAlertDeny(string sessionKey)
        {
            if (sessionKey.ContainsInAlertKeys())
            {
                Session[sessionKey] = null;
            }
        }
    }
}