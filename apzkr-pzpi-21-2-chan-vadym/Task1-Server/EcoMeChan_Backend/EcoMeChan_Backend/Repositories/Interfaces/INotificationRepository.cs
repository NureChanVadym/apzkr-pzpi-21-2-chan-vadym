// INotificationRepository.cs


using EcoMeChan.Models;

namespace EcoMeChan.Repositories.Interfaces
{
    public interface INotificationRepository
    {
        Task<Notification> CreateAsync(Notification notification);
        Task<Notification> GetAsync(int notificationId);
        Task<IEnumerable<Notification>> GetAllAsync();
        Task<IEnumerable<Notification>> GetByUserIdAsync(int userId);
        Task<Notification> UpdateAsync(Notification notification);
        Task DeleteAsync(int notificationId);
    }
}