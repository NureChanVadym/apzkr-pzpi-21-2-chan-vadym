// ConsumptionNormCalculator.cs


using EcoMeChan.Models;
using System.Collections.Generic;
using System.Linq;

namespace EcoMeChan.Services.ConsumptionNorms
{
    public abstract class ConsumptionNormCalculator
    {
        public ConsumptionNorm CalculateNorm(IEnumerable<Consumption> consumptionData)
        {
            if (consumptionData == null || !consumptionData.Any())
            {
                throw new ArgumentException("Consumption data cannot be null or empty.", nameof(consumptionData));
            }

            var baseline = CalculateBaseline(consumptionData);
            var deviation = CalculateDeviation(consumptionData, baseline);

            return new ConsumptionNorm
            {
                BaselineConsumption = baseline,
                StandardDeviation = deviation
            };
        }

        protected abstract decimal CalculateDeviation(IEnumerable<Consumption> consumptionData, decimal baseline);

        protected decimal CalculateBaseline(IEnumerable<Consumption> consumptionData)
        {
            if (consumptionData == null || !consumptionData.Any())
            {
                throw new ArgumentException("Consumption data cannot be null or empty.", nameof(consumptionData));
            }

            return consumptionData.Average(c => c.ConsumedAmount);
        }

        public abstract bool IsAnomaly(decimal currentConsumption, decimal baselineConsumption, decimal deviation);
    }
}