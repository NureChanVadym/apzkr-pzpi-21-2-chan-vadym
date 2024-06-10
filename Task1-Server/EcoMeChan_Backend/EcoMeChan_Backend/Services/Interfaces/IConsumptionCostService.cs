// IConsumptionCostService.cs


using System;
using System.Threading.Tasks;

namespace EcoMeChan.Services.Interfaces
{
    public interface IConsumptionCostService
    {
        Task<decimal> CalculateTotalCostAsync(int userId, DateTime startDate, DateTime endDate);
    }
}