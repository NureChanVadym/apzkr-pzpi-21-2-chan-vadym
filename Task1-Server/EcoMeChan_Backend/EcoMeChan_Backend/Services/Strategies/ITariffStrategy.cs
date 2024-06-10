// ITariffStrategy.cs


using EcoMeChan.Models;

namespace EcoMeChan.Services.Strategies
{
    public interface ITariffStrategy
    {
        decimal CalculateCost(decimal consumedAmount, Tariff tariff);
    }
}