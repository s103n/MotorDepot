using MotorDepot.BLL.Interfaces;
using MotorDepot.WEB.Infrastructure.Mappers;
using MotorDepot.WEB.Models;
using MotorDepot.WEB.Models.Enums;
using System.Collections.Generic;
using System.Net;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace MotorDepot.WEB.Controllers
{
    //[Authorize(Roles = "admin")]
    public class AutoController : Controller
    {
        private readonly IAutoService _autoService;
        private readonly List<AlertViewModel> _alerts = new List<AlertViewModel>();

        public AutoController(IAutoService autoService)
        {
            _autoService = autoService;
        }

        public async Task<ActionResult> Create()
        {
            var brandOperation = await _autoService.GetBrandsAsync();
            var typesOperation = _autoService.GetAutoTypes();

            ViewBag.AutoBrands = new SelectList(brandOperation.Value, "Id", "Name");
            ViewBag.AutoTypes = new SelectList(typesOperation.Value, "Id", "Name");

            return View();
        }

        [HttpPost]
        public async Task<ActionResult> Create(AutoCreateViewModel auto)
        {
            var operation = await _autoService.CreateAutoAsync(auto.ToDto());

            if (operation.Success)
            {
                _alerts.Add(new AlertViewModel(
                    $"Auto with numbers [{auto.Numbers}] was created",
                    AlertType.Success));

                return View("~/Views/Home/Index.cshtml", _alerts);
            }
            
            return RedirectToAction("Create");
        }

        protected override void Dispose(bool disposing)
        {
            _autoService.Dispose();
            base.Dispose(disposing);
        }
    }
}