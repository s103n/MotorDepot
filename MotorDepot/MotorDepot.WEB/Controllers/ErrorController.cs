using System.Web.Mvc;

namespace MotorDepot.WEB.Controllers
{
    public class ErrorController : Controller
    {
        // GET: Error
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult NotFound()
        {
            Response.StatusCode = 404;
            return View();
        }

        public ActionResult Oops()
        {
            Response.StatusCode = 500;
            return View();
        }
    }
}