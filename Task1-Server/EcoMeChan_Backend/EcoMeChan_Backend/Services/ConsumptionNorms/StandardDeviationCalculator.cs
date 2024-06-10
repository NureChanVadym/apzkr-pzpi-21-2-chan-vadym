// StandardDeviationCalculator.cs


using EcoMeChan.Models;
using EcoMeChan.Services.ConsumptionNorms;
using System;
using System.Collections.Generic;
using System.Linq;

namespace EcoMeChan.Services.ConsumptionNorms
{
    public class StandardDeviationCalculator : ConsumptionNormCalculator
    {
        protected override decimal CalculateDeviation(IEnumerable<Consumption> consumptionData, decimal baseline)
        {
            var consumptions = consumptionData.Select(c => c.ConsumedAmount).ToList();
            var sumOfSquares = consumptions.Sum(c => (c - baseline) * (c - baseline));
            var variance = sumOfSquares / (consumptions.Count - 1);
            return (decimal)Math.Sqrt((double)variance);
        }

        public override bool IsAnomaly(decimal currentConsumption, decimal baselineConsumption, decimal deviation)
        {
            return Math.Abs(currentConsumption - baselineConsumption) > 2 * deviation;
        }
    }
}

