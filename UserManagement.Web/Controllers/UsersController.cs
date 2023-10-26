using System.Linq;
using UserManagement.Services.Domain.Interfaces;
using UserManagement.Web.Models.Users;
using UserManagement.Models;
using UserManagement.Data;

namespace UserManagement.WebMS.Controllers;

[Route("users")]
public class UsersController : Controller
{
    private readonly IUserService _userService;
    private readonly IDataContext _dataContext;

    public UsersController(IUserService userService, IDataContext dataContext)
    {
        _userService = userService;
        _dataContext = dataContext;
    }

    [HttpGet("list")]
    public ViewResult List(bool? showActive = null)
    {
        // Get the list of users based on the showActive parameter
        IEnumerable<User> users;

        if (showActive.HasValue)
        {
            users = showActive.Value
                ? _userService.FilterByActive(true)  // Fetch active users
                : _userService.FilterByActive(false); // Fetch non-active users
        }
        else
        {
            // If showActive is not specified, get all users
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

    [HttpGet("add")]
    public IActionResult AddUser()
    {
        // Return a view that displays a form for adding a new user
        return View();
    }

    [HttpPost]
    public IActionResult Create(User user)
    {
        if (ModelState.IsValid)
        {
            // Add the user to the data context
            _dataContext.Create(user);
            return RedirectToAction("List");
        }
        return View(user);
    }

    [HttpGet("delete/{id}")] // Use a route parameter to specify the user ID
    public IActionResult Delete(int id)
    {
        var user = _dataContext.GetAll<User>().FirstOrDefault(u => u.Id == id);

        if (user == null)
        {
            return NotFound(); // Handle user not found
        }

        return View("DeleteUser", user);
    }

    [HttpPost("delete/{id}")] // Use a route parameter for the user ID
    public IActionResult DeleteConfirmed(int id)
    {
        var user = _dataContext.GetAll<User>().FirstOrDefault(u => u.Id == id);

        if (user == null)
        {
            return NotFound(); // Handle user not found
        }

        _dataContext.Delete(user);

        return RedirectToAction("List"); // Redirect to the list page after deletion
    }
}
