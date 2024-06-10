// IConsumptionService.cs


using EcoMeChan.ViewModels;
using EcoMeChan.ViewModels.Create;
using EcoMeChan.ViewModels.Edit;
using EcoMeChan_Backend.ViewModels;

namespace EcoMeChan.Services.Interfaces
{
    public interface IConsumptionService
    {
        Task<ConsumptionViewModel> CreateAsync(ConsumptionCreateViewModel consumptionCreateViewModel);
        Task<IEnumerable<ConsumptionViewModel>> GetByUserIdAsync(int userId);
        Task<ConsumptionViewModel> GetAsync(int consumptionId);
        Task<ConsumptionViewModel> UpdateAsync(int consumptionId, ConsumptionEditViewModel consumptionEditViewModel);
        Task DeleteAsync(int consumptionId);
    }
}