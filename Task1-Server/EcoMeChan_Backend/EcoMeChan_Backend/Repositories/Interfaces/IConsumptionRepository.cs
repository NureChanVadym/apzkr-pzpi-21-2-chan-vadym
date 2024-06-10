// IConsumptionRepository.cs


using EcoMeChan.Enums;
using EcoMeChan.Models;

namespace EcoMeChan.Repositories.Interfaces
{
    public interface IConsumptionRepository
    {
        Task<Consumption> CreateAsync(Consumption consumption);
        Task<IEnumerable<Consumption>> GetByUserIdAsync(int userId);
        Task<IEnumerable<Consumption>> GetByUserIdAsync(int userId, DateTime startDate, DateTime endDate);
        Task<IEnumerable<Consumption>> GetByUserIdAsync(int userId, int resourceTypeId, DateTime startDate, DateTime endDate);
        Task<Consumption> GetAsync(int consumptionId);
        Task<IEnumerable<Consumption>> GetAllAsync();
        Task<Consumption> UpdateAsync(Consumption consumption);
        Task DeleteAsync(int consumptionId);
    }
}