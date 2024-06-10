// IoTDeviceViewModel.cs
using EcoMeChan.Enums;

namespace EcoMeChan.ViewModels
{
    public class IoTDeviceViewModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public IoTDeviceType Type { get; set; }
        public bool IsActive { get; set; }
    }
}