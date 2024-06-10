// ResourceType.cs

using System.Collections.Generic;

namespace EcoMeChan.Models
{
    public class ResourceType
    {
        public int Id { get; set; }
        public ResourceTypeEnum Type { get; set; }
        public string Unit { get; set; }

        public ICollection<Tariff> Tariffs { get; set; }
    }

    public enum ResourceTypeEnum
    {
        Water,
        Gas,
        Electricity
    }
}