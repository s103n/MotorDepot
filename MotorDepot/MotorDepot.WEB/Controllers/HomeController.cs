using MotorDepot.WEB.Infrastructure;
using System.Web.Mvc;

namespace MotorDepot.WEB.Controllers
{
    [Authorize]
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