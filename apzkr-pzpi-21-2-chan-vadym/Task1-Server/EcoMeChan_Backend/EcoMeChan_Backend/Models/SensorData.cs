// SensorData.cs

namespace EcoMeChan.Models
{
    public class SensorData
    {
        public int Id { get; set; }
        public int IoTDeviceId { get; set; }
        public DateTime Timestamp { get; set; }
        public decimal Value { get; set; }

        public IoTDevice IoTDevice { get; set; }
    }
}