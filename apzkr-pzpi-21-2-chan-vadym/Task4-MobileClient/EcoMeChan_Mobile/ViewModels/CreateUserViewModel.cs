using System.Collections.Generic;
using System.Windows.Input;
using EcoMeChan_Mobile.Enums;
using EcoMeChan_Mobile.Models;
using EcoMeChan_Mobile.Services;

namespace EcoMeChan_Mobile.ViewModels
{
    public class CreateUserViewModel : BaseViewModel
    {
        private readonly UserService _userService;
        private AdminAccountManagementViewModel _adminViewModel;

        public User NewUser { get; set; }
        public string Password { get; set; }
        public List<string> Roles { get; }
        public ICommand CreateUserCommand { get; }

        public string LoginPlaceholder { get; set; }
        public string EmailPlaceholder { get; set; }
        public string FirstNamePlaceholder { get; set; }
        public string LastNamePlaceholder { get; set; }
        public string MiddleNamePlaceholder { get; set; }
        public string PhonePlaceholder { get; set; }
        public string PasswordPlaceholder { get; set; }
        public string RolePickerTitle { get; set; }

        public CreateUserViewModel()
        {
            _userService = new UserService();
            NewUser = new User();
            Roles = new List<string>
            {
                Role.User.ToString(),
                Role.MunicipalResourceManager.ToString()
            };
            CreateUserCommand = new Command(async () => await CreateUser());
        }

        public void SetAdminViewModel(AdminAccountManagementViewModel adminViewModel)
        {
            _adminViewModel = adminViewModel;
        }

        private async Task CreateUser()
        {
            var roleString = NewUser.Role.ToString();
            NewUser.Role = (Role)Enum.Parse(typeof(Role), roleString);

            var createdUser = await _userService.CreateUser(NewUser, Password);
            if (createdUser != null)
            {
                _adminViewModel.Users.Add(createdUser);
                await Application.Current.MainPage.DisplayAlert("Success", "User created successfully", "OK");
                await Shell.Current.GoToAsync("..");
            }
            else
            {
                await Application.Current.MainPage.DisplayAlert("Error", "Failed to create user", "OK");
            }
        }
    }
}