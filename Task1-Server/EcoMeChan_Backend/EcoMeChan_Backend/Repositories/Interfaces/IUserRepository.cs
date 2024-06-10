// IUserRepository.cs


using EcoMeChan.Models;

namespace EcoMeChan.Repositories.Interfaces
{
    public interface IUserRepository
    {
        Task<User> CreateAsync(User user);
        Task<User> GetAsync(int userId);
        Task<User> GetByLoginAsync(string login);
        Task<IEnumerable<User>> GetAllAsync();
        Task<User> UpdateAsync(User user);
        Task DeleteAsync(int userId);
    }
}