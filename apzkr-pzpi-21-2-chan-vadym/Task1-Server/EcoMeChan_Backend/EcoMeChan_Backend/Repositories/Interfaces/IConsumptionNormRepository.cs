// IConsumptionNormRepository.cs

using EcoMeChan.Models;
using System.Threading.Tasks;

namespace EcoMeChan.Repositories.Interfaces
{
    public interface IConsumptionNormRepository
    {
        Task<ConsumptionNorm> GetByUserIdAsync(int userId, int resourceTypeId);
        Task<ConsumptionNorm> CreateAsync(ConsumptionNorm consumptionNorm);
        Task<ConsumptionNorm> UpdateAsync(ConsumptionNorm consumptionNorm);
    }
}