// Tariff.cs

namespace EcoMeChan.Models
{
    public class Tariff
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int ResourceTypeId { get; set; }
        public decimal PricePerUnit { get; set; }
        public int CurrencyId { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }

        public ResourceType ResourceType { get; set; }
        public Currency Currency { get; set; }
        public ICollection<Consumption> Consumptions { get; set; }
    }
}