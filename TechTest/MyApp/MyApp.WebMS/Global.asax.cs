using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;
using MyApp.WebMS.Factories;

namespace MyApp.WebMS
{
    public class MvcApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);

            RegisterCustomControllerFactory();
        }



        private void RegisterCustomControllerFactory()
        {
            ControllerBuilder.Current.SetControllerFactory(new ControllerFactory());
        }
    }
}
