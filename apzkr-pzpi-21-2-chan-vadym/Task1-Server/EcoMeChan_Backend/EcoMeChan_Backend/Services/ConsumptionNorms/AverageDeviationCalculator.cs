// AverageDeviationCalculator.cs


using EcoMeChan.Models;
using EcoMeChan.Services.ConsumptionNorms;
using System;
using System.Collections.Generic;
using System.Linq;

namespace EcoMeChan.Services.ConsumptionNorms
{
    public class AverageDeviationCalculator : ConsumptionNormCalculator
    {
        protected override decimal CalculateDeviation(IEnumerable<Consumption> consumptionData, decimal baseline)
        {
            var consumptions = consumptionData.Select(c => c.ConsumedAmount).ToList();
            var sumOfDeviations = consumptions.Sum(c => Math.Abs(c - baseline));
            return sumOfDeviations / consumptions.Count;
        }

        public override bool IsAnomaly(decimal currentConsumption, decimal baselineConsumption, decimal deviation)
        {
            return Math.Abs(currentConsumption - baselineConsumption) > 3 * deviation;
        }
    }
}
