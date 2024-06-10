// ViewModels/TariffViewModel.cs
using System;

namespace EcoMeChan.ViewModels
{
    public class TariffViewModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int ResourceTypeId { get; set; }
        public decimal PricePerUnit { get; set; }
        public int CurrencyId { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
    }
}