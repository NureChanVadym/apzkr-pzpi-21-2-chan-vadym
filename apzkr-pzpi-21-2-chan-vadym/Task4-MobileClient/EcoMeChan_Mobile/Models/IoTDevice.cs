// Models/IoTDevice.cs
using EcoMeChan_Mobile.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace EcoMeChan_Mobile.Models
{
    public class IoTDevice
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public ResourceType Type { get; set; }
        public bool IsActive { get; set; }
    }
}