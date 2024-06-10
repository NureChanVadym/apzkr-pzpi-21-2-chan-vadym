// SensorDataViewModel.cs
using System;

namespace EcoMeChan.ViewModels
{
    public class SensorDataViewModel
    {
        public int Id { get; set; }
        public int IoTDeviceId { get; set; }
        public DateTime Timestamp { get; set; }
        public decimal Value { get; set; }
    }
}