// ConsumptionNormRepository.cs
using EcoMeChan.Database;
using EcoMeChan.Enums;
using EcoMeChan.Models;
using EcoMeChan.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Threading.Tasks;

namespace EcoMeChan.Repositories
{
    public class ConsumptionNormRepository : IConsumptionNormRepository
    {
        private readonly ApplicationDbContext _context;

        public ConsumptionNormRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<ConsumptionNorm> GetByUserIdAsync(int userId, int resourceTypeId)
        {
            return await _context.ConsumptionNorms
                .FirstOrDefaultAsync(cn => cn.UserId == userId && cn.ResourceTypeId == resourceTypeId);
        }

        public async Task<ConsumptionNorm> CreateAsync(ConsumptionNorm consumptionNorm)
        {
            _context.ConsumptionNorms.Add(consumptionNorm);
            await _context.SaveChangesAsync();
            return consumptionNorm;
        }

        public async Task<ConsumptionNorm> UpdateAsync(ConsumptionNorm consumptionNorm)
        {
            _context.ConsumptionNorms.Update(consumptionNorm);
            await _context.SaveChangesAsync();
            return consumptionNorm;
        }
    }
}