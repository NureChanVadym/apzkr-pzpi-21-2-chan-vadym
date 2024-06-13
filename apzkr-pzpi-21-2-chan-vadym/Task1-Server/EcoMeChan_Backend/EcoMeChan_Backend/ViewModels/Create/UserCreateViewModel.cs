// UserCreateViewModel.cs
using EcoMeChan.Enums;

namespace EcoMeChan.ViewModels.Create
{
    public class UserCreateViewModel
    {
        public string Login { get; set; }
        public string Password { get; set; }
        public string Phone { get; set; }
        public string Email { get; set; }
        public string LastName { get; set; }
        public string FirstName { get; set; }
        public string MiddleName { get; set; }
        public Role Role { get; set; }
    }
}