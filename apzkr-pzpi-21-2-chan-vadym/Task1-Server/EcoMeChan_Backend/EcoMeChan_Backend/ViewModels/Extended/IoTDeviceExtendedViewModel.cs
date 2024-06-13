// IoTDeviceExtendedViewModel.cs
using EcoMeChan.Enums;
using System.Collections.Generic;

namespace EcoMeChan.ViewModels.Extended
{
    public class IoTDeviceExtendedViewModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public IoTDeviceType Type { get; set; }
        public bool IsActive { get; set; }
        public List<NotificationViewModel> Notifications { get; set; }
        public List<SensorDataViewModel> SensorData { get; set; }
    }
}