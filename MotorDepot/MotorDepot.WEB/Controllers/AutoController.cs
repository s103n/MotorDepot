using MotorDepot.BLL.Interfaces;
using MotorDepot.WEB.Infrastructure;
using MotorDepot.WEB.Infrastructure.Mappers;
using MotorDepot.WEB.Models;
using MotorDepot.WEB.Models.Auto;
using MotorDepot.WEB.Models.Enums;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace MotorDepot.WEB.Controllers
{
    [Authorize(Roles = "admin")]
    public class AutoController : Controller
    {
        private readonly IAutoService _autoService;
        private readonly IFlightService _flightService;

        public AutoController(IAutoService autoService,
            IFlightService flightService)
        {
            _flightService = flightService;
            _autoService = autoService;
        }

        public async Task<ActionResult> Create()
        {
            var brands = await _autoService.GetBrandsAsync();
            var types = _autoService.GetAutoTypes();

            ViewBag.AutoBrands = new SelectList(brands, "Id", "Name");
            ViewBag.AutoTypes = new SelectList(types, "Id", "Name");

            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create(AutoCreateViewModel auto)
        {
            if (ModelState.IsValid)
            {
                var operation = await _autoService.CreateAutoAsync(auto.ToDto());

                if (operation.Success)
                {
                    Session["Create"] = new AlertViewModel(operation.Message, AlertType.Success);

                    return RedirectToAction("Index", "Home");
                }

                return new HttpOperationStatusResult(operation);
            }

            return View(auto);
        }

        public async Task<ActionResult> All()
        {
            var autos = await _autoService.GetAutosAsync();
            var flights = await _flightService.GetAllAsync();

            return View(autos.ToDisplayViewModel(flights.Value));
        }

        public async Task<ActionResult> Edit(int? autoId)
        {
            var operation = await _autoService.GetAutoById(autoId);

            if (operation.Success)
            {
                ViewBag.AutoTypes = new SelectList(_autoService.GetAutoTypes(), "Id", "Name");
                ViewBag.AutoBrands = new SelectList(await _autoService.GetBrandsAsync(), "Id", "Name");

                return View(operation.Value.ToEditViewModel());
            }

            return new HttpOperationStatusResult(operation);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit(AutoEditViewModel auto)
        {
            var operation = await _autoService.EditAutoAsync(auto.ToDto());

            if (operation.Success)
            {
                Session["Update"] = new AlertViewModel(operation.Message, AlertType.Success);

                return RedirectToAction("Index", "Home");
            }

            return new HttpOperationStatusResult(operation);
        }

        protected override void Dispose(bool disposing)
        {
            _autoService.Dispose();
            base.Dispose(disposing);
        }
    }
}