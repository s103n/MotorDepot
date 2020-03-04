using MotorDepot.BLL.Interfaces;
using MotorDepot.WEB.Infrastructure.Mappers;
using MotorDepot.WEB.Models;
using System.Net;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace MotorDepot.WEB.Controllers
{
    //[Authorize(Roles = "admin")]
    public class AdminController : Controller
    {
        private readonly IDispatcherService _dispatcherService;
        private readonly IDriverService _driverService;
        public AdminController(IDispatcherService dispatcherService, IDriverService driverService)
        {
            _driverService = driverService;
            _dispatcherService = dispatcherService;
        }

        public ActionResult AddDispatcher()
        {
            return View("AddUser");
        }

        [HttpPost]
        public async Task<ActionResult> AddDispatcher(UserRegisterViewModel model)
        {
            if (ModelState.IsValid)
            {
                var operation = await _dispatcherService.CreateDispatcher(model.ToUserDto("dispatcher"));

                if (operation.Success)
                {
                    //todo alert
                    return View("~/Views/Home/Index.cshtml");
                }

                ModelState.AddModelError("", operation.Message);
            }

            return View("AddUser", model);
        }

        public ActionResult Dispatchers()
        {
            var operation = _dispatcherService.GetDispatchers();

            if (operation.Success)
            {
                return View(operation.Value);
            }

            return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
        }

        public ActionResult AddDriver()
        {
            return View("AddUser");
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> AddDriver(UserRegisterViewModel model)
        {
            if (ModelState.IsValid)
            {
                var operation = await _driverService.CreateDriver(model.ToUserDto("driver"));

                if (operation.Success)
                {
                    //todo alert
                    return View("~/Views/Home/Index.cshtml");
                }

                ModelState.AddModelError("", operation.Message);
            }

            return View("AddUser", model);
        }

        protected override void Dispose(bool disposing)
        {
            _dispatcherService.Dispose();
            _driverService.Dispose();
            base.Dispose(disposing);
        }
    }
}