// CurrencyRepository.cs
using EcoMeChan.Database;
using EcoMeChan.Models;
using EcoMeChan.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

public class CurrencyRepository : ICurrencyRepository
{
    private readonly ApplicationDbContext _context;

    public CurrencyRepository(ApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<IEnumerable<Currency>> GetAllAsync()
    {
        return await _context.Currencies.ToListAsync();
    }

    public async Task<Currency> GetByIdAsync(int id)
    {
        return await _context.Currencies.FindAsync(id);
    }

    public async Task<Currency> CreateAsync(Currency currency)
    {
        _context.Currencies.Add(currency);
        await _context.SaveChangesAsync();
        return currency;
    }

    public async Task<Currency> UpdateAsync(Currency currency)
    {
        _context.Currencies.Update(currency);
        await _context.SaveChangesAsync();
        return currency;
    }

    public async Task DeleteAsync(int id)
    {
        var currency = await _context.Currencies.FindAsync(id);
        if (currency != null)
        {
            _context.Currencies.Remove(currency);
            await _context.SaveChangesAsync();
        }
    }
}