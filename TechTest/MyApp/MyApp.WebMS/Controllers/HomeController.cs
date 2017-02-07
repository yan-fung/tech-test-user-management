using System.Linq;
using System.Web.Mvc;
using MyApp.Models;
using MyApp.Services.Factories.Interfaces;
using MyApp.WebMS.Controllers.Base;

namespace MyApp.WebMS.Controllers
{
    public class HomeController : BaseController
    {
        public HomeController(IServiceFactory serviceFactory) : base(serviceFactory) { }

        public ActionResult Index()
        {
            return View();
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
    }
}