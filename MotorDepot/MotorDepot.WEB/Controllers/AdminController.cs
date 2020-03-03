using System.Net;
using MotorDepot.BLL.Infrastructure;
using MotorDepot.BLL.Interfaces;
using MotorDepot.WEB.Infrastructure.Mappers;
using MotorDepot.WEB.Models;
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
                var status = await _dispatcherService.CreateDispatcher(model.ToUserDto("dispatcher"));

                if (status.Code == HttpStatusCode.OK)
                    return View("~/Views/Home/Index.cshtml");

                ModelState.AddModelError("", status.Message);
            }

            return View("AddUser", model);
        }

        public ActionResult Dispatchers()
        {
            return View(_dispatcherService.GetDispatchers());
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
                var status = await _driverService.CreateDriver(model.ToUserDto("driver"));

                if (status.Code == HttpStatusCode.OK)
                    return View("Index", status);

                ModelState.AddModelError("", status.Message);
            }

            return View("AddUser", model);
        }
    }
}