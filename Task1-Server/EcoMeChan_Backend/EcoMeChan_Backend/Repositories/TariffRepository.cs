// TariffRepository.cs


using EcoMeChan.Database;
using EcoMeChan.Models;
using EcoMeChan.Enums;
using EcoMeChan.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;



namespace EcoMeChan.Repositories
{
    public class TariffRepository : ITariffRepository
    {
        private readonly ApplicationDbContext _context;

        public TariffRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<Tariff> CreateAsync(Tariff tariff)
        {
            _context.Tariffs.Add(tariff);
            await _context.SaveChangesAsync();
            return tariff;
        }

        public async Task<Tariff> GetAsync(int tariffId)
        {
            return await _context.Tariffs
                .Include(t => t.Consumptions)
                .FirstOrDefaultAsync(t => t.Id == tariffId);
        }

        public async Task<IEnumerable<Tariff>> GetAllAsync()
        {
            return await _context.Tariffs
                .Include(t => t.Consumptions)
                .ToListAsync();
        }

        public async Task<IEnumerable<Tariff>> GetByResourceTypeIdAsync(int resourceTypeId)
        {
            return await _context.Tariffs
                .Include(t => t.Consumptions)
                .Where(t => t.ResourceTypeId == resourceTypeId)
                .ToListAsync();
        }

        public async Task<Tariff?> GetByResourceTypeAndDateAsync(ResourceTypeEnum resourceType, DateTime date)
        {
            return await _context.Tariffs
                .FirstOrDefaultAsync(t => t.ResourceType.Type == resourceType && t.StartDate <= date && t.EndDate >= date);
        }

        public async Task<Tariff> UpdateAsync(Tariff tariff)
        {
            _context.Tariffs.Update(tariff);
            await _context.SaveChangesAsync();
            return tariff;
        }

        public async Task DeleteAsync(int tariffId)
        {
            var tariff = await _context.Tariffs.FindAsync(tariffId);
            if (tariff != null)
            {
                _context.Tariffs.Remove(tariff);
                await _context.SaveChangesAsync();
            }
        }
    }
}