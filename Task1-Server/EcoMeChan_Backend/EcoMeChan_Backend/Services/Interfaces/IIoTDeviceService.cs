// IIoTDeviceService.cs


using EcoMeChan.ViewModels;
using EcoMeChan.ViewModels.Create;
using EcoMeChan.ViewModels.Edit;
using EcoMeChan.ViewModels.Extended;

namespace EcoMeChan.Services.Interfaces
{
    public interface IIoTDeviceService
    {
        Task<IoTDeviceViewModel> CreateAsync(IoTDeviceCreateViewModel deviceCreateViewModel);
        Task<IoTDeviceExtendedViewModel> GetAsync(int deviceId);
        Task<IEnumerable<IoTDeviceViewModel>> GetByUserIdAsync(int userId);

        Task<IEnumerable<IoTDeviceViewModel>> GetAllAsync();
        Task<IoTDeviceViewModel> UpdateAsync(int deviceId, IoTDeviceEditViewModel deviceEditViewModel);
        Task DeleteAsync(int deviceId);
    }
}