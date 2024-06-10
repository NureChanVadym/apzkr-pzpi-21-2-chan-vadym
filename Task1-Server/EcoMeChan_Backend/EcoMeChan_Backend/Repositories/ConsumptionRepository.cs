// ConsumptionRepository.cs


using EcoMeChan.Database;
using EcoMeChan.Enums;
using EcoMeChan.Models;
using EcoMeChan.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;


namespace EcoMeChan.Repositories
{
    public class ConsumptionRepository : IConsumptionRepository
    {
        private readonly ApplicationDbContext _context;

        public ConsumptionRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<Consumption> CreateAsync(Consumption consumption)
        {
            _context.Consumptions.Add(consumption);
            await _context.SaveChangesAsync();
            return consumption;
        }

        public async Task<IEnumerable<Consumption>> GetByUserIdAsync(int userId)
        {
            return await _context.Consumptions
                .Include(c => c.Tariff)
                    .ThenInclude(t => t.ResourceType)
                .Include(c => c.Tariff)
                    .ThenInclude(t => t.Currency)
                .Where(c => c.UserId == userId)
                .ToListAsync();
        }

        public async Task<IEnumerable<Consumption>> GetByUserIdAsync(int userId, DateTime startDate, DateTime endDate)
        {
            return await _context.Consumptions
                .Where(c => c.UserId == userId &&
                            c.Date >= startDate &&
                            c.Date <= endDate)
                .ToListAsync();
        }

        public async Task<IEnumerable<Consumption>> GetByUserIdAsync(int userId, int resourceTypeId, DateTime startDate, DateTime endDate)
        {
            return await _context.Consumptions
                .Where(c => c.UserId == userId && c.Tariff.ResourceTypeId == resourceTypeId && c.Date >= startDate && c.Date <= endDate)
                .ToListAsync();
        }

        public async Task<Consumption> GetAsync(int consumptionId)
        {
            return await _context.Consumptions
                .FirstOrDefaultAsync(c => c.Id == consumptionId);
        }

        public async Task<IEnumerable<Consumption>> GetAllAsync()
        {
            return await _context.Consumptions
                .Include(c => c.User)
                .Include(c => c.Tariff)
                .ToListAsync();
        }

        public async Task<Consumption> UpdateAsync(Consumption consumption)
        {
            _context.Consumptions.Update(consumption);
            await _context.SaveChangesAsync();
            return consumption;
        }

        public async Task DeleteAsync(int consumptionId)
        {
            var consumption = await _context.Consumptions.FindAsync(consumptionId);
            if (consumption != null)
            {
                _context.Consumptions.Remove(consumption);
                await _context.SaveChangesAsync();
            }
        }
    }
}