using System.Collections.Generic;
using System.Windows.Input;
using EcoMeChan_Mobile.Enums;
using EcoMeChan_Mobile.Models;
using EcoMeChan_Mobile.Services;

namespace EcoMeChan_Mobile.ViewModels
{
    public class AdminEditUserViewModel : BaseViewModel
    {
        private readonly UserService _userService;

        public User User { get; set; }
        public ICommand UpdateUserCommand { get; }
        public List<string> Roles { get; }

        public string LoginPlaceholder { get; set; }
        public string EmailPlaceholder { get; set; }
        public string FirstNamePlaceholder { get; set; }
        public string LastNamePlaceholder { get; set; }
        public string MiddleNamePlaceholder { get; set; }
        public string PhonePlaceholder { get; set; }

        public AdminEditUserViewModel(User user)
        {
            _userService = new UserService();
            User = user;
            UpdateUserCommand = new Command(async () => await UpdateUser());
            Roles = new List<string>
            {
                Role.User.ToString(),
                Role.Admin.ToString(),
                Role.MunicipalResourceManager.ToString()
            };
        }

        private async Task UpdateUser()
        {
            var updateSuccess = await _userService.UpdateUser(User.Id, User);
            if (updateSuccess)
            {
                await Application.Current.MainPage.DisplayAlert("Success", "User information updated successfully", "OK");
                await Shell.Current.GoToAsync("..");
            }
            else
            {
                await Application.Current.MainPage.DisplayAlert("Error", "Failed to update user information", "OK");
            }
        }
    }
}