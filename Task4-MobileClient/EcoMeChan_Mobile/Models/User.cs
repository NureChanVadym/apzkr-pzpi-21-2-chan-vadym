// Models/User.cs
using EcoMeChan_Mobile.Enums;

namespace EcoMeChan_Mobile.Models
{
    public class User
    {
        public int Id { get; set; }
        public string Login { get; set; }
        public string Phone { get; set; }
        public string Email { get; set; }
        public string LastName { get; set; }
        public string FirstName { get; set; }
        public string MiddleName { get; set; }
        public Role Role { get; set; }
    }
}