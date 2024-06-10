// Notification.cs

using EcoMeChan.Enums;

namespace EcoMeChan.Models
{
    public class Notification
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public int IoTDeviceId { get; set; }
        public NotificationType NotificationType { get; set; }
        public string Text { get; set; }
        public DateTime CreatedAt { get; set; }

        public User User { get; set; }
        public IoTDevice IoTDevice { get; set; }
    }
}