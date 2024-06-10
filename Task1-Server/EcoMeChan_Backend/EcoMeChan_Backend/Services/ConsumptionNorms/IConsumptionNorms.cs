using System;
using System.Threading.Tasks;

namespace EcoMeChan_Backend.Services.ConsumptionNorms
{
    public interface IConsumptionNormService
    {
        Task CalculateConsumptionNormAsync(int userId, int resourceTypeId, DateTime startDate, DateTime endDate);
        Task<bool> DetectAnomaliesAsync(int userId, int resourceTypeId, decimal currentConsumption);
    }
}