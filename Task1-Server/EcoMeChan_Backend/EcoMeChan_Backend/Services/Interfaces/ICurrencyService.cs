// ICurrencyService.cs
using EcoMeChan.ViewModels;
using EcoMeChan.ViewModels.Create;
using EcoMeChan.ViewModels.Edit;

public interface ICurrencyService
{
    Task<IEnumerable<CurrencyViewModel>> GetAllAsync();
    Task<CurrencyViewModel> GetByIdAsync(int id);
    Task<CurrencyViewModel> CreateAsync(CurrencyCreateViewModel currencyCreateViewModel);
    Task<CurrencyViewModel> UpdateAsync(int id, CurrencyEditViewModel currencyEditViewModel);
    Task DeleteAsync(int id);
}