using EcoMeChan_Mobile.Models;
using EcoMeChan_Mobile.Resources.Languages;
using EcoMeChan_Mobile.ViewModels;
using System.Windows.Input;

namespace EcoMeChan_Mobile.Views
{
    public partial class AdminAccountManagementPage : ContentPage
    {
        private readonly AdminAccountManagementViewModel _viewModel;
        public ICommand EditUserCommand { get; }

        public AdminAccountManagementPage()
        {
            InitializeComponent();
            _viewModel = new AdminAccountManagementViewModel();
            BindingContext = _viewModel;

            _viewModel.UpdateUserCommand = new Command<User>(async (user) => await UpdateUser(user));
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();
            _viewModel.LoadUsersCommand.Execute(null);
            UpdateLocalization();
        }

        private async Task UpdateUser(User user)
        {
            await Navigation.PushAsync(new AdminEditUserPage(user));
        }

        private async void CreateUserButton_Clicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new CreateUserPage(_viewModel));
        }

        private void UpdateLocalization()
        {
            Title = AppResources.AdminAccountManagementTitle;
            PageTitleLabel.Text = AppResources.AdminAccountManagementTitle;
            CreateUserButton.Text = AppResources.CreateUserButtonText;
        }

        private async void NavigateToLanguageSelection(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new LanguageSelectionPage());
        }
    }
}