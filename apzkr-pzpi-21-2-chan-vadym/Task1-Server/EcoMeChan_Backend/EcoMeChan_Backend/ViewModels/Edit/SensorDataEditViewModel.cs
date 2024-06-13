// SensorDataEditViewModel.cs
using System;

namespace EcoMeChan.ViewModels.Edit
{
    public class SensorDataEditViewModel
    {
        public int Id { get; set; }
        public int IoTDeviceId { get; set; }
        public DateTime Timestamp { get; set; }
        public decimal Value { get; set; }
    }
}