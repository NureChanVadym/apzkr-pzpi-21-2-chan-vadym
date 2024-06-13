// ISensorDataRepository.cs


using EcoMeChan.Models;

namespace EcoMeChan.Repositories.Interfaces
{
    public interface ISensorDataRepository
    {
        Task<SensorData> CreateAsync(SensorData sensorData);
        Task<SensorData> GetAsync(int sensorDataId);
        Task<IEnumerable<SensorData>> GetAllAsync();
        Task<IEnumerable<SensorData>> GetByDeviceIdAsync(int deviceId);
        Task<SensorData> UpdateAsync(SensorData sensorData);
        Task DeleteAsync(int sensorDataId);
    }
}