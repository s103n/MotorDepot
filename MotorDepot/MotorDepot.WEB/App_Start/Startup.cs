using Microsoft.AspNet.Identity;
using Microsoft.Owin;
using Microsoft.Owin.Security.Cookies;
using MotorDepot.BLL.Interfaces;
using MotorDepot.BLL.Services;
using MotorDepot.BLL.Util;
using Ninject;
using Ninject.Web.Common.OwinHost;
using Owin;

[assembly: OwinStartup(typeof(MotorDepot.App_Start.Startup))]

namespace MotorDepot.App_Start
{
    public sealed class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            app.UseCookieAuthentication(new CookieAuthenticationOptions
            {
                AuthenticationType = DefaultAuthenticationTypes.ApplicationCookie,
                LoginPath = new PathString("/Account/Login"),
            });

            app.UseNinjectMiddleware(CreateKernel);
        }

        private static StandardKernel CreateKernel()
        {
            var kernel = new StandardKernel(new UnitOfWorkModule());

            kernel.Bind<IUserService>().To<UserService>();
            kernel.Bind<IDispatcherService>().To<DispatcherService>();
            kernel.Bind<IDriverService>().To<DriverService>();
            kernel.Bind<IAutoService>().To<AutoService>();
            kernel.Bind<IFlightService>().To<FlightService>();

            return kernel;
        }
    }
}