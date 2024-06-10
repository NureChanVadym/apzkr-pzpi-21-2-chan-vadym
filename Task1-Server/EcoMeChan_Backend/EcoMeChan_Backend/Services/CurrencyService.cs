// CurrencyService.cs
using AutoMapper;
using EcoMeChan.Models;
using EcoMeChan.Repositories.Interfaces;
using EcoMeChan.Services.Interfaces;
using EcoMeChan.ViewModels;
using EcoMeChan.ViewModels.Create;
using EcoMeChan.ViewModels.Edit;

public class CurrencyService : ICurrencyService
{
    private readonly ICurrencyRepository _currencyRepository;
    private readonly IMapper _mapper;

    public CurrencyService(ICurrencyRepository currencyRepository, IMapper mapper)
    {
        _currencyRepository = currencyRepository;
        _mapper = mapper;
    }

    public async Task<IEnumerable<CurrencyViewModel>> GetAllAsync()
    {
        var currencies = await _currencyRepository.GetAllAsync();
        return _mapper.Map<IEnumerable<CurrencyViewModel>>(currencies);
    }

    public async Task<CurrencyViewModel> GetByIdAsync(int id)
    {
        var currency = await _currencyRepository.GetByIdAsync(id);
        return _mapper.Map<CurrencyViewModel>(currency);
    }

    public async Task<CurrencyViewModel> CreateAsync(CurrencyCreateViewModel currencyCreateViewModel)
    {
        var currency = _mapper.Map<Currency>(currencyCreateViewModel);
        var createdCurrency = await _currencyRepository.CreateAsync(currency);
        return _mapper.Map<CurrencyViewModel>(createdCurrency);
    }

    public async Task<CurrencyViewModel> UpdateAsync(int id, CurrencyEditViewModel currencyEditViewModel)
    {
        var existingCurrency = await _currencyRepository.GetByIdAsync(id);
        if (existingCurrency == null)
        {
            throw new InvalidOperationException("Currency not found.");
        }

        _mapper.Map(currencyEditViewModel, existingCurrency);
        var updatedCurrency = await _currencyRepository.UpdateAsync(existingCurrency);
        return _mapper.Map<CurrencyViewModel>(updatedCurrency);
    }

    public async Task DeleteAsync(int id)
    {
        await _currencyRepository.DeleteAsync(id);
    }
}