// ITariffService.cs


using EcoMeChan.Enums;
using EcoMeChan.ViewModels;
using EcoMeChan.ViewModels.Create;
using EcoMeChan.ViewModels.Edit;

namespace EcoMeChan.Services.Interfaces
{
    public interface ITariffService
    {
        Task<TariffViewModel> CreateAsync(TariffCreateViewModel tariffCreateViewModel);
        Task<TariffViewModel> GetAsync(int tariffId);
        Task<IEnumerable<TariffViewModel>> GetAllAsync();
        Task<IEnumerable<TariffViewModel>> GetByResourceTypeIdAsync(int resourceTypeId);
        Task<TariffViewModel> UpdateAsync(int tariffId, TariffEditViewModel tariffEditViewModel);
        Task DeleteAsync(int tariffId);
    }
}