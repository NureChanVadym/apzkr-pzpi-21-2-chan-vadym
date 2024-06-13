// UserRepository.cs


using EcoMeChan.Database;
using EcoMeChan.Models;
using EcoMeChan.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace EcoMeChan.Repositories
{
    public class UserRepository : IUserRepository
    {
        private readonly ApplicationDbContext _context;

        public UserRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<User> CreateAsync(User user)
        {
            _context.Users.Add(user);
            await _context.SaveChangesAsync();
            return user;
        }

        public async Task<User> GetAsync(int userId)
        {
            return await _context.Users
                .Include(u => u.Consumptions)
                .Include(u => u.Notifications)
                .FirstOrDefaultAsync(u => u.Id == userId);
        }

        public async Task<User> GetByLoginAsync(string login)
        {
            return await _context.Users
                .Include(u => u.Consumptions)
                .Include(u => u.Notifications)
                .FirstOrDefaultAsync(u => u.Login == login);
        }

        public async Task<IEnumerable<User>> GetAllAsync()
        {
            return await _context.Users
                .Include(u => u.Consumptions)
                .Include(u => u.Notifications)
                .ToListAsync();
        }

        public async Task<User> UpdateAsync(User user)
        {
            _context.Users.Update(user);
            await _context.SaveChangesAsync();
            return user;
        }

        public async Task DeleteAsync(int userId)
        {
            var user = await _context.Users.FindAsync(userId);
            if (user != null)
            {
                _context.Users.Remove(user);
                await _context.SaveChangesAsync();
            }
        }
    }
}