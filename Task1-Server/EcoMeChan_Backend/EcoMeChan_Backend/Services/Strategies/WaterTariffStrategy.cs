// WaterTariffStrategy.cs


using EcoMeChan.Enums;
using EcoMeChan.Models;

namespace EcoMeChan.Services.Strategies
{
    public class WaterTariffStrategy : ITariffStrategy
    {
        public decimal CalculateCost(decimal consumedAmount, Tariff tariff)
        {
            if (tariff.ResourceType.Type != ResourceTypeEnum.Water)
            {
                throw new InvalidOperationException("Invalid tariff type for water consumption.");
            }

            return consumedAmount * tariff.PricePerUnit;
        }
    }
}