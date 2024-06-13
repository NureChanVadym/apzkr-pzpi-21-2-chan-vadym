// ICurrencyRepository.cs
using EcoMeChan.Models;

public interface ICurrencyRepository
{
    Task<IEnumerable<Currency>> GetAllAsync();
    Task<Currency> GetByIdAsync(int id);
    Task<Currency> CreateAsync(Currency currency);
    Task<Currency> UpdateAsync(Currency currency);
    Task DeleteAsync(int id);
}