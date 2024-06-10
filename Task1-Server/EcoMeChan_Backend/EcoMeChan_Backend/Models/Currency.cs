// Currency.cs

using System.Collections.Generic;

namespace EcoMeChan.Models
{
    public class Currency
    {
        public int Id { get; set; }
        public string Code { get; set; }

        public ICollection<Tariff> Tariffs { get; set; }
    }
}