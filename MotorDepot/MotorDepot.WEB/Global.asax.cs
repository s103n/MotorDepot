using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;

namespace MotorDepot.WEB
{
    public class MvcApplication : System.Web.HttpApplication
    {
        //TODO Переписать всю валидацию из веба в уровень доступа к данным
        //TODO Переписать все под ValidationErrors и OperationStatus
        //TODO Продолжать по заданию
        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);
        }
    }
}
