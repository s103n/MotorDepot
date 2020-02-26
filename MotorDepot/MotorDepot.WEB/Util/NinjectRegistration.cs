using MotorDepot.BLL.Util;
using Ninject;
using Ninject.Web.Mvc;
using System.Web.Mvc;

namespace MotorDepot.WEB.Util
{
    public class NinjectRegistration
    {
        public static void RegisterDependencies()
        {
            var databaseModule = new UnitOfWorkModule();
            var registerModule = new RegisterModule();
            var kernel = new StandardKernel(databaseModule, registerModule);
            DependencyResolver.SetResolver(new NinjectDependencyResolver(kernel));
        }
    }
}