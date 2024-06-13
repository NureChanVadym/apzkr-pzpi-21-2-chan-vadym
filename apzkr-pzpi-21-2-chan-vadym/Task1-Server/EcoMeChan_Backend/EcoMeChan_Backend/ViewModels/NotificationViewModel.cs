// NotificationViewModel.cs
using EcoMeChan.Enums;
using System;

namespace EcoMeChan.ViewModels
{
    public class NotificationViewModel
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public int IoTDeviceId { get; set; }
        public NotificationType NotificationType { get; set; }
        public string Text { get; set; }
        public DateTime CreatedAt { get; set; }
    }
}