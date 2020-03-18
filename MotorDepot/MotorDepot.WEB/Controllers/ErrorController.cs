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

        public ActionResult NotFound(string message)
        { 
            Response.StatusCode = 404;
            return View(message);
        }

        public ActionResult Oops(string message)
        {
            Response.StatusCode = 500;
            return View(message);
        }

        public ActionResult BadRequest(string message)
        {
            Response.StatusCode = 400;
            return View(message);
        }
    }
}