// PeriodConsumption.cs


using System.Collections.Generic;
using System.Linq;

namespace EcoMeChan.Models.TimeConsumption
{
    public class PeriodConsumption : IConsumptionComponent
    {
        private readonly List<DayConsumption> _dayConsumptions = new List<DayConsumption>();

        public void AddDayConsumption(DayConsumption dayConsumption)
        {
            _dayConsumptions.Add(dayConsumption);
        }

        public decimal CalculateCost()
        {
            return _dayConsumptions.Sum(d => d.CalculateCost());
        }
    }
}