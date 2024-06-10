// INotificationService.cs


using EcoMeChan.ViewModels;
using EcoMeChan.ViewModels.Create;
using EcoMeChan.ViewModels.Edit;

namespace EcoMeChan.Services.Interfaces
{
    public interface INotificationService
    {
        Task<NotificationViewModel> CreateAsync(NotificationCreateViewModel notificationCreateViewModel);
        Task<NotificationViewModel> GetAsync(int notificationId);
        Task<IEnumerable<NotificationViewModel>> GetAllAsync();
        Task<IEnumerable<NotificationViewModel>> GetByUserIdAsync(int userId);
        Task<NotificationViewModel> UpdateAsync(int notificationId, NotificationEditViewModel notificationEditViewModel);
        Task DeleteAsync(int notificationId);
    }
}