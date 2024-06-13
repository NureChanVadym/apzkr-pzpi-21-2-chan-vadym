// ISensorDataService.cs


using EcoMeChan.ViewModels;
using EcoMeChan.ViewModels.Create;
using EcoMeChan.ViewModels.Edit;

namespace EcoMeChan.Services.Interfaces
{
    public interface ISensorDataService
    {
        Task<SensorDataViewModel> CreateAsync(SensorDataCreateViewModel sensorDataCreateViewModel);
        Task<SensorDataViewModel> GetAsync(int sensorDataId);
        Task<IEnumerable<SensorDataViewModel>> GetAllAsync();
        Task<IEnumerable<SensorDataViewModel>> GetByDeviceIdAsync(int deviceId);
        Task<SensorDataViewModel> UpdateAsync(int sensorDataId, SensorDataEditViewModel sensorDataEditViewModel);
        Task DeleteAsync(int sensorDataId);
    }
}