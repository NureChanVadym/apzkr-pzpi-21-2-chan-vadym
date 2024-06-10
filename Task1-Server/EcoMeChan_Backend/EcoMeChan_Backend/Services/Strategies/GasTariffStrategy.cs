// GasTariffStrategy.cs


using EcoMeChan.Enums;
using EcoMeChan.Models;

namespace EcoMeChan.Services.Strategies
{
    public class GasTariffStrategy : ITariffStrategy
    {
        public decimal CalculateCost(decimal consumedAmount, Tariff tariff)
        {
            if (tariff.ResourceType.Type != ResourceTypeEnum.Gas)
            {
                throw new InvalidOperationException("Invalid tariff type for gas consumption.");
            }

            return consumedAmount * tariff.PricePerUnit;
        }
    }
}