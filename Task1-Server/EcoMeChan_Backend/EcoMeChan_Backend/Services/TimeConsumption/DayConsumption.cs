// DayConsumption.cs


using EcoMeChan.Enums;
using System;

namespace EcoMeChan.Models.TimeConsumption
{
    public class DayConsumption : IConsumptionComponent
    {
        public DateTime Date { get; set; }
        public ResourceType ResourceType { get; set; }
        public decimal ConsumedAmount { get; set; }
        public Tariff Tariff { get; set; }

        public decimal CalculateCost()
        {
            return ConsumedAmount * Tariff.PricePerUnit;
        }
    }
}