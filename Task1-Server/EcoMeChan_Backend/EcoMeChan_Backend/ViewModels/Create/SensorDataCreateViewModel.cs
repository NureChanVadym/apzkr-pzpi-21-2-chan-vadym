// SensorDataCreateViewModel.cs
using System;

namespace EcoMeChan.ViewModels.Create
{
    public class SensorDataCreateViewModel
    {
        public int IoTDeviceId { get; set; }
        public DateTime Timestamp { get; set; }
        public decimal Value { get; set; }
    }
}