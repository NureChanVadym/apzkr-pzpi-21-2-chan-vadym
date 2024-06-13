// IoTDeviceCreateViewModel.cs
using EcoMeChan.Enums;

namespace EcoMeChan.ViewModels.Create
{
    public class IoTDeviceCreateViewModel
    {
        public string Name { get; set; }
        public IoTDeviceType Type { get; set; }
        public bool IsActive { get; set; }
    }
}