// ConsumptionCostService.cs

using EcoMeChan.Enums;
using EcoMeChan.Models.TimeConsumption;
using EcoMeChan.Repositories.Interfaces;
using EcoMeChan.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EcoMeChan.Services
{
    public class ConsumptionCostService : IConsumptionCostService
    {
        private readonly IConsumptionRepository _consumptionRepository;
        private readonly ITariffRepository _tariffRepository;

        public ConsumptionCostService(IConsumptionRepository consumptionRepository, ITariffRepository tariffRepository)
        {
            _consumptionRepository = consumptionRepository;
            _tariffRepository = tariffRepository;
        }

        public async Task<decimal> CalculateTotalCostAsync(int userId, DateTime startDate, DateTime endDate)
        {
            var consumptions = await _consumptionRepository.GetByUserIdAsync(userId, startDate, endDate);
            var periodConsumption = new PeriodConsumption();

            foreach (var consumption in consumptions)
            {
                var tariff = await _tariffRepository.GetByResourceTypeAndDateAsync(consumption.Tariff.ResourceType.Type, consumption.Date);
                var dayConsumption = new DayConsumption
                {
                    Date = consumption.Date,
                    ResourceType = consumption.Tariff.ResourceType,
                    ConsumedAmount = consumption.ConsumedAmount,
                    Tariff = tariff
                };
                periodConsumption.AddDayConsumption(dayConsumption);
            }

            return periodConsumption.CalculateCost();
        }
    }
}