using System.Web.Mvc;
using Microsoft.AspNet.Identity;
using Microsoft.Owin;
using Microsoft.Owin.Security.Cookies;
using MotorDepot.BLL.Interfaces;
using MotorDepot.BLL.Services;
using MotorDepot.BLL.Util;
using MotorDepot.WEB.Filters;
using Ninject;
using Ninject.Web.Common.OwinHost;
using Ninject.Web.Mvc.FilterBindingSyntax;
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

            //Service register
            kernel.Bind<IUserService>().To<UserService>();
            kernel.Bind<IDispatcherService>().To<DispatcherService>();
            kernel.Bind<IDriverService>().To<DriverService>();
            kernel.Bind<IAutoService>().To<AutoService>();
            kernel.Bind<IFlightService>().To<FlightService>();
            kernel.Bind<IFlightRequestService>().To<FlightRequestService>();
            kernel.Bind<ILoggerService>().To<LogService>();

            //Filter register
            kernel.BindFilter<ExceptionLoggerFilter>(FilterScope.Controller, 0)
                .WhenControllerHas<ExceptionLoggerAttribute>();
            kernel.BindFilter<ActionLoggerFilter>(FilterScope.Controller, 0)
                .WhenControllerHas<ActionLoggerAttribute>();

            return kernel;
        }
    }
}