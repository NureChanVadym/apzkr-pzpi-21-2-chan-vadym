// ViewModels/ConsumptionViewModel.cs
using EcoMeChan.Models;
using System;

namespace EcoMeChan.ViewModels
{
    public class ConsumptionViewModel
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public int TariffId { get; set; }
        public DateTime Date { get; set; }
        public decimal ConsumedAmount { get; set; }
        public decimal ConsumedCost { get; set; }
        public ResourceTypeEnum ResourceType { get; set; }
        public string CurrencyCode { get; set; }
        public string Unit { get; set; }
    }
}