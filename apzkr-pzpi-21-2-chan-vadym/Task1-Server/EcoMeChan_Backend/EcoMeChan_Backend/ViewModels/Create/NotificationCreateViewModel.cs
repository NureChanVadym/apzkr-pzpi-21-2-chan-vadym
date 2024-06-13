// NotificationCreateViewModel.cs
using EcoMeChan.Enums;

namespace EcoMeChan.ViewModels.Create
{
    public class NotificationCreateViewModel
    {
        public int UserId { get; set; }
        public int IoTDeviceId { get; set; }
        public NotificationType NotificationType { get; set; }
        public DateTime CreatedAt { get; set; }

        public string Text { get; set; }
    }
}