using System.Linq;
using System.Web.Mvc;
using MyApp.Services.Factories.Interfaces;
using MyApp.WebMS.Controllers.Base;
using MyApp.WebMS.Models;

namespace MyApp.WebMS.Controllers
{
    [RoutePrefix("users")]
    public class UsersController : BaseController
    {
        public UsersController(IServiceFactory serviceFactory) : base(serviceFactory) { }

        [Route("", Name = "UserList")]
        public ActionResult List()
        {
            var items = ServiceFactory.UserService.GetAll().Select(p => new UserListItemViewModel
            {
                Id = p.Id,
                Forename = p.Forename,
                Surname = p.Surname,
                Email = p.Email,
                IsActive = p.IsActive
            });

            var model = new UserListViewModel
            {
                Items = items.ToList()
            };

            return View("List", model);
        }
    }
}