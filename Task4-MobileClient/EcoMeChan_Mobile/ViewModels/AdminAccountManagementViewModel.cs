// ViewModels/AdminAccountManagementViewModel.cs
using System.Collections.ObjectModel;
using System.Windows.Input;
using EcoMeChan_Mobile.Models;
using EcoMeChan_Mobile.Services;

namespace EcoMeChan_Mobile.ViewModels
{
    public class AdminAccountManagementViewModel : BaseViewModel
    {
        private readonly UserService _userService;

        public ObservableCollection<User> Users { get; }
        public string Password { get; set; }
        public ICommand LoadUsersCommand { get; }
        public ICommand DeleteUserCommand { get; }
        public ICommand UpdateUserCommand { get; set; }
        public ICommand CreateUserCommand { get; }

        public AdminAccountManagementViewModel()
        {
            _userService = new UserService();
            Users = new ObservableCollection<User>();
            LoadUsersCommand = new Command(async () => await LoadUsers());
            DeleteUserCommand = new Command<User>(async (user) => await DeleteUser(user));
            UpdateUserCommand = new Command<User>(async (user) => await UpdateUser(user));
            CreateUserCommand = new Command<User>(async (user) => await CreateUser(user));
        }

        private async Task LoadUsers()
        {
            var users = await _userService.GetAllUsers();
            Users.Clear();
            foreach (var user in users)
            {
                Users.Add(user);
            }
        }

        private async Task DeleteUser(User user)
        {
            var isDeleted = await _userService.DeleteUser(user.Id);
            if (isDeleted)
            {
                Users.Remove(user);
            }
        }

        private async Task UpdateUser(User user)
        {
            var updateSuccess = await _userService.UpdateUser(user.Id, user);
            if (updateSuccess)
            {
                var index = Users.IndexOf(user);
                Users.RemoveAt(index);
                Users.Insert(index, user);
            }
        }

        private async Task CreateUser(User user)
        {
            var createdUser = await _userService.CreateUser(user, Password);
            if (createdUser != null)
            {
                Users.Add(createdUser);
                Password = string.Empty; 
            }
        }
    }
}