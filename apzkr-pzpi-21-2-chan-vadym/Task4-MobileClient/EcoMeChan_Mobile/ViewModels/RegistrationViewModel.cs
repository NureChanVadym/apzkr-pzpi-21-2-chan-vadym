// ViewModels/RegistrationViewModel.cs
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Text;
using EcoMeChan_Mobile.Enums;
using EcoMeChan_Mobile.Models;
using EcoMeChan_Mobile.Services;
using Newtonsoft.Json;

namespace EcoMeChan_Mobile.ViewModels
{
    public class RegistrationViewModel : INotifyPropertyChanged
    {
        private User user;
        private string password;
        private AuthService authService;

        public RegistrationViewModel()
        {
            user = new User();
            authService = new AuthService();
        }

        public string Login
        {
            get => user.Login;
            set
            {
                if (user.Login != value)
                {
                    user.Login = value;
                    OnPropertyChanged();
                }
            }
        }

        public string Password
        {
            get => password;
            set
            {
                if (password != value)
                {
                    password = value;
                    OnPropertyChanged();
                }
            }
        }

        public string Phone
        {
            get => user.Phone;
            set
            {
                if (user.Phone != value)
                {
                    user.Phone = value;
                    OnPropertyChanged();
                }
            }
        }

        public string Email
        {
            get => user.Email;
            set
            {
                if (user.Email != value)
                {
                    user.Email = value;
                    OnPropertyChanged();
                }
            }
        }

        public string LastName
        {
            get => user.LastName;
            set
            {
                if (user.LastName != value)
                {
                    user.LastName = value;
                    OnPropertyChanged();
                }
            }
        }

        public string FirstName
        {
            get => user.FirstName;
            set
            {
                if (user.FirstName != value)
                {
                    user.FirstName = value;
                    OnPropertyChanged();
                }
            }
        }

        public string MiddleName
        {
            get => user.MiddleName;
            set
            {
                if (user.MiddleName != value)
                {
                    user.MiddleName = value;
                    OnPropertyChanged();
                }
            }
        }

        public Command RegisterCommand => new Command(async () =>
        {
            var registrationData = new
            {
                login = user.Login,
                password = password,
                phone = user.Phone,
                email = user.Email,
                lastName = user.LastName,
                firstName = user.FirstName,
                middleName = user.MiddleName,
                role = (int)Role.User 
            };

            var json = JsonConvert.SerializeObject(registrationData);
            var content = new StringContent(json, Encoding.UTF8, "application/json");

            var registeredUser = await authService.RegisterAsync(content);
            if (registeredUser != null)
            {
                // Успішна реєстрація, перехід на сторінку входу
                await Shell.Current.GoToAsync("//LoginPage");
            }
            else
            {
                // Обробка помилки реєстрації
                await Application.Current.MainPage.DisplayAlert("Registration Error", "Failed to register user", "OK");
            }
        });

        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}