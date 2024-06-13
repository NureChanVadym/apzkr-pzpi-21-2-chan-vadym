// IoTDeviceRepository.cs


using EcoMeChan.Database;
using EcoMeChan.Models;
using EcoMeChan.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;


namespace EcoMeChan.Repositories
{
    public class IoTDeviceRepository : IIoTDeviceRepository
    {
        private readonly ApplicationDbContext _context;

        public IoTDeviceRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<IoTDevice> CreateAsync(IoTDevice device)
        {
            _context.IoTDevices.Add(device);
            await _context.SaveChangesAsync();
            return device;
        }

        public async Task<IoTDevice> GetAsync(int deviceId)
        {
            return await _context.IoTDevices
                .FirstOrDefaultAsync(d => d.Id == deviceId);
        }

        public async Task<IEnumerable<IoTDevice>> GetByUserIdAsync(int userId)
        {
            return await _context.IoTDevices
                .Where(d => d.Notifications.Any(n => n.UserId == userId))
                .Include(d => d.Notifications)
                .Include(d => d.SensorData)
                .ToListAsync();
        }

        public async Task<IEnumerable<IoTDevice>> GetAllAsync()
        {
            return await _context.IoTDevices
                .Include(d => d.Notifications)
                .Include(d => d.SensorData)
                .ToListAsync();
        }

        public async Task<IoTDevice> UpdateAsync(IoTDevice device)
        {
            _context.IoTDevices.Update(device);
            await _context.SaveChangesAsync();
            return device;
        }

        public async Task DeleteAsync(int deviceId)
        {
            var device = await _context.IoTDevices.FindAsync(deviceId);
            if (device != null)
            {
                _context.IoTDevices.Remove(device);
                await _context.SaveChangesAsync();
            }
        }
    }
}