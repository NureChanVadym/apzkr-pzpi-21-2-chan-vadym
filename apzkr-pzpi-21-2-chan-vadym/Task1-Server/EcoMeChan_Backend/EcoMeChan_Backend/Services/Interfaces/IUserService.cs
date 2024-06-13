// IUserService.cs


using EcoMeChan.ViewModels;
using EcoMeChan.ViewModels.Create;
using EcoMeChan.ViewModels.Edit;
using EcoMeChan.ViewModels.Extended;

namespace EcoMeChan.Services.Interfaces
{
    public interface IUserService
    {
        Task<UserViewModel> CreateAsync(UserCreateViewModel userCreateViewModel);
        Task<UserViewModel> AuthenticateAsync(string login, string password);
        Task<UserExtendedViewModel> GetAsync(int userId);
        Task<UserExtendedViewModel> GetByLoginAsync(string login);
        Task<IEnumerable<UserViewModel>> GetAllAsync();
        Task<UserViewModel> UpdateAsync(int userId, UserEditViewModel userEditViewModel);
        Task DeleteAsync(int userId);
        public string HashPassword(string password);
        void Logout();
    }
}