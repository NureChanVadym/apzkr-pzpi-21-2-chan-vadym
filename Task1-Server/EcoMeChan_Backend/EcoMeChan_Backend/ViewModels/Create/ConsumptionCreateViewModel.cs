// ViewModels/Create/ConsumptionCreateViewModel.cs
using System;

namespace EcoMeChan.ViewModels.Create
{
    public class ConsumptionCreateViewModel
    {
        public int UserId { get; set; }
        public int TariffId { get; set; }
        public DateTime Date { get; set; }
        public decimal ConsumedAmount { get; set; }
    }
}