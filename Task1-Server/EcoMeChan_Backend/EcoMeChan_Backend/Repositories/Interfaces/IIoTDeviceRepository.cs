// IIoTDeviceRepository.cs


using EcoMeChan.Models;

namespace EcoMeChan.Repositories.Interfaces
{
    public interface IIoTDeviceRepository
    {
        Task<IoTDevice> CreateAsync(IoTDevice device);
        Task<IoTDevice> GetAsync(int deviceId);
        Task<IEnumerable<IoTDevice>> GetByUserIdAsync(int userId);
        Task<IEnumerable<IoTDevice>> GetAllAsync();
        Task<IoTDevice> UpdateAsync(IoTDevice device);
        Task DeleteAsync(int deviceId);
    }
}