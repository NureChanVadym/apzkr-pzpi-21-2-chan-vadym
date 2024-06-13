// UserExtendedViewModel.cs
using EcoMeChan.Enums;
using System.Collections.Generic;

namespace EcoMeChan.ViewModels.Extended
{
    public class UserExtendedViewModel
    {
        public int Id { get; set; }
        public string Login { get; set; }
        public string Phone { get; set; }
        public string Email { get; set; }
        public string LastName { get; set; }
        public string FirstName { get; set; }
        public string MiddleName { get; set; }
        public Role Role { get; set; }
        public List<ConsumptionViewModel> Consumptions { get; set; }
        public List<NotificationViewModel> Notifications { get; set; }
    }
}