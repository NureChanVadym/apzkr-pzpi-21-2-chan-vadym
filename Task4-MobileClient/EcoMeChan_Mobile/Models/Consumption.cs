using EcoMeChan_Mobile.Enums;
using System;

namespace EcoMeChan_Mobile.Models
{
    public class Consumption
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public int TariffId { get; set; }
        public DateTime Date { get; set; }
        public decimal ConsumedAmount { get; set; }
        public decimal ConsumedCost { get; set; }
        public ResourceType ResourceType { get; set; }
        public string CurrencyCode { get; set; }
        public string Unit { get; set; }

        // Formatted fields
        public string FormattedDate { get; set; }
        public string FormattedTime { get; set; }
        public string FormattedResourceType { get; set; }
        public string FormattedAmount { get; set; }
        public string FormattedCost { get; set; }
    }
}