// ViewModels/LoginViewModel.cs
using System.ComponentModel;
using System.Runtime.CompilerServices;
using EcoMeChan_Mobile.Enums;
using EcoMeChan_Mobile.Models;
using EcoMeChan_Mobile.Services;

namespace EcoMeChan_Mobile.ViewModels
{
    public class LoginViewModel : INotifyPropertyChanged
    {
        private LoginCredentials credentials;
        private AuthService authService;

        public LoginViewModel()
        {
            credentials = new LoginCredentials();
            authService = new AuthService();
        }

        public string Login
        {
            get => credentials.Login;
            set
            {
                if (credentials.Login != value)
                {
                    credentials.Login = value;
                    OnPropertyChanged();
                }
            }
        }

        public string Password
        {
            get => credentials.Password;
            set
            {
                if (credentials.Password != value)
                {
                    credentials.Password = value;
                    OnPropertyChanged();
                }
            }
        }

        public Command LoginCommand => new Command(async () =>
        {
            var user = await authService.LoginAsync(credentials);
            if (user != null)
            {
                // Успішна авторизація, перехід на відповідну сторінку в залежності від ролі користувача
                if (user.Role == Role.User)
                {
                    await Shell.Current.GoToAsync("//UserTab/UserAccountPage");
                }
                else if (user.Role == Role.Admin)
                {
                    await Shell.Current.GoToAsync("//AdminTab/AdminAccountPage");
                }
            }
            else
            {
                // Обробка помилки авторизації
                await Application.Current.MainPage.DisplayAlert("Login Error", "Invalid login or password", "OK");
            }
        });

        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}