using Microsoft.Owin.Security;
using MotorDepot.BLL.Interfaces;
using MotorDepot.WEB.Infrastructure.Mappers;
using MotorDepot.WEB.Models;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace MotorDepot.WEB.Controllers
{
    public class AccountController : Controller
    {
        private readonly IUserService _userService;
        private IAuthenticationManager _authenticationManager
        => HttpContext.GetOwinContext().Authentication;
        public AccountController(IUserService userService)
        {
            _userService = userService;
        }

        public ActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public async Task<ActionResult> Login(LoginViewModel model)
        {
            if (ModelState.IsValid)
            {
                var claim = await _userService.Authenticate(model.ToUserDto());

                if (claim != null)
                {
                    _authenticationManager.SignOut();
                    _authenticationManager.SignIn(new AuthenticationProperties
                    {
                        IsPersistent = true
                    }, claim);

                    return RedirectToAction("Index", "Home");
                }

                ModelState.AddModelError("", "E-mail or password are not correct.");
            }

            return View(model);
        }

        public ActionResult Logout()
        {
            _authenticationManager.SignOut();

            return RedirectToAction("Login");
        }
    }
}