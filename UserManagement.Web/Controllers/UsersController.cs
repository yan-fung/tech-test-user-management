using System.Linq;
using UserManagement.Services.Domain.Interfaces;
using UserManagement.Web.Models.Users;
using UserManagement.Models;

namespace UserManagement.WebMS.Controllers;

[Route("users")]
public class UsersController : Controller
{
    private readonly IUserService _userService;
    public UsersController(IUserService userService) => _userService = userService;

    [HttpGet]
    public ViewResult List(bool? showActive = null)
    {
        /// Get the list of users based on the showActive parameter
        IEnumerable<User> users;

        if (showActive.HasValue)
        {
            users = showActive.Value
                ? _userService.FilterByActive(true)  /// Fetch active users
                : _userService.FilterByActive(false); /// Fetch non-active users
        }
        else
        {
            /// If showActive is not specified, get all users
            users = _userService.GetAll();
        }

        var items = users.Select(p => new UserListItemViewModel
        {
            Id = p.Id,
            Forename = p.Forename,
            Surname = p.Surname,
            DateOfBirth = p.DateOfBirth.Date.ToString("yyyy-MM-dd"),
            Email = p.Email,
            IsActive = p.IsActive
        });

        var model = new UserListViewModel
        {
            Items = items.ToList()
        };

        return View(model);
    }
}
