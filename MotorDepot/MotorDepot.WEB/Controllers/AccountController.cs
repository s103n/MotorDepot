using Microsoft.Owin.Security;
using MotorDepot.BLL.Interfaces;
using MotorDepot.WEB.Infrastructure.Mappers;
using MotorDepot.WEB.Models.Auto;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using MotorDepot.WEB.Models;
using MotorDepot.WEB.Models.Enums;

namespace MotorDepot.WEB.Controllers
{
    [Authorize]
    public class AccountController : Controller
    {
        private readonly IUserService _userService;
        private IAuthenticationManager AuthenticationManager
            => HttpContext.GetOwinContext().Authentication;

        public AccountController(IUserService userService)
        {
            _userService = userService;
        }

        [AllowAnonymous]
        public ActionResult Login()
        {
            return View();
        }

        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        [HttpPost]
        public async Task<ActionResult> Login(LoginViewModel model)
        {
            if (ModelState.IsValid)
            {
                var claimOperation = await _userService.Authenticate(model.ToUserDto());

                if (claimOperation.Success)
                {
                    AuthenticationManager.SignOut();
                    AuthenticationManager.SignIn(new AuthenticationProperties
                    {
                        IsPersistent = true //todo
                    }, claimOperation.Value);

                    Session["Authenticate"] = new AlertViewModel(claimOperation.Message, AlertType.Success);

                    return RedirectToAction("Index", "Home");
                }

                ModelState.AddModelError("", claimOperation.Message);
            }

            return View(model);
        }

        public ActionResult Logout()
        {
            AuthenticationManager.SignOut();
            Session.Clear();

            return RedirectToAction("Login");
        }

        protected override void Dispose(bool disposing)
        {
            _userService.Dispose();
            base.Dispose(disposing);
        }
    }
}