using System.Web.Mvc;
using MyApp.Services.Factories.Interfaces;

namespace MyApp.WebMS.Controllers.Base
{
    public abstract class BaseController : Controller
    {
        protected BaseController(IServiceFactory serviceFactory)
        {
            ServiceFactory = serviceFactory;
        }

        protected IServiceFactory ServiceFactory { get; }
    }
}