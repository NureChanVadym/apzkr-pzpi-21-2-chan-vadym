// ViewModels/Create/TariffCreateViewModel.cs
using System;

namespace EcoMeChan.ViewModels.Create
{
    public class TariffCreateViewModel
    {
        public string Name { get; set; }
        public int ResourceTypeId { get; set; }
        public decimal PricePerUnit { get; set; }
        public int CurrencyId { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
    }
}