using System.Collections.Generic;
namespace MyApp.WebMS.Models
{
    public class UserListViewModel
    {
        public IList<UserListItemViewModel> Items { get; set; } 
    }

    public class UserListItemViewModel
    {
        public long Id { get; set; }

        public string Forename { get; set; }

        public string Surname { get; set; }

        public string Email { get; set; }

        public bool IsActive { get; set; }
    }
}