// ConsumptionNorm.cs

using EcoMeChan.Enums;
using System;

namespace EcoMeChan.Models
{
    public class ConsumptionNorm
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public int ResourceTypeId { get; set; }
        public decimal BaselineConsumption { get; set; }
        public decimal StandardDeviation { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
    }
}