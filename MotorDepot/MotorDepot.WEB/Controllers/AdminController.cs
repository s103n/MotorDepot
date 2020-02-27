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

        public AdminController(IDispatcherService dispatcherService)
        {
            _dispatcherService = dispatcherService;
        }

        // GET: Admin
        public ActionResult Index()
        {
            return View(new OperationStatus("", "", false));
        }

        public ActionResult AddDispatcher()
        {
            return View();
        }

        [HttpPost]
        public async Task<ActionResult> AddDispatcher(DispatcherRegisterViewModel model)
        {
            if (ModelState.IsValid)
            {
                var status = await _dispatcherService.CreateDispatcher(model.ToUserDto());

                if (status.Success)
                    return View("Index", status);

                ModelState.AddModelError(status.Property, status.Message);
            }

            return View(model);
        }

        public ActionResult Dispatchers()
        {
            return View(_dispatcherService.GetDispatchers());
        }
    }
}