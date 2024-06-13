// SensorDataRepository.cs


using EcoMeChan.Database;
using EcoMeChan.Models;
using EcoMeChan.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;


namespace EcoMeChan.Repositories
{
    public class SensorDataRepository : ISensorDataRepository
    {
        private readonly ApplicationDbContext _context;

        public SensorDataRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<SensorData> CreateAsync(SensorData sensorData)
        {
            _context.SensorData.Add(sensorData);
            await _context.SaveChangesAsync();
            return sensorData;
        }

        public async Task<SensorData?> GetAsync(int sensorDataId)
        {
            return await _context.SensorData
                .Include(sd => sd.IoTDevice)
                .FirstOrDefaultAsync(sd => sd.Id == sensorDataId);
        }

        public async Task<IEnumerable<SensorData>> GetAllAsync()
        {
            return await _context.SensorData
                .Include(sd => sd.IoTDevice)
                .ToListAsync();
        }

        public async Task<IEnumerable<SensorData>> GetByDeviceIdAsync(int deviceId)
        {
            return await _context.SensorData
                .Include(sd => sd.IoTDevice)
                .Where(sd => sd.IoTDeviceId == deviceId)
                .ToListAsync();
        }

        public async Task<SensorData> UpdateAsync(SensorData sensorData)
        {
            _context.SensorData.Update(sensorData);
            await _context.SaveChangesAsync();
            return sensorData;
        }

        public async Task DeleteAsync(int sensorDataId)
        {
            var sensorData = await _context.SensorData.FindAsync(sensorDataId);
            if (sensorData != null)
            {
                _context.SensorData.Remove(sensorData);
                await _context.SaveChangesAsync();
            }
        }
    }
}