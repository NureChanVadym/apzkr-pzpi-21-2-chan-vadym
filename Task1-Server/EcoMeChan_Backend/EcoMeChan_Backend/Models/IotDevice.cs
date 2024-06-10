// IoTDevice.cs

using EcoMeChan.Enums;

namespace EcoMeChan.Models
{
    public class IoTDevice
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public IoTDeviceType Type { get; set; }
        public bool IsActive { get; set; }

        public ICollection<Notification> Notifications { get; set; }
        public ICollection<SensorData> SensorData { get; set; }
    }
}