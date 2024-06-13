// ElectricityTariffStrategy.cs

using EcoMeChan.Enums;
using EcoMeChan.Models;

namespace EcoMeChan.Services.Strategies
{
    public class ElectricityTariffStrategy : ITariffStrategy
    {
        public decimal CalculateCost(decimal consumedAmount, Tariff tariff)
        {
            if (tariff.ResourceType.Type != ResourceTypeEnum.Electricity)
            {
                throw new InvalidOperationException("Invalid tariff type for electricity consumption.");
            }
            return consumedAmount * tariff.PricePerUnit;
        }
    }
}