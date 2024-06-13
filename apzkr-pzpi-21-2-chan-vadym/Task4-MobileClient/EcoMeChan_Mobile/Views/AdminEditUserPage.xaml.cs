using EcoMeChan_Mobile.Models;
using EcoMeChan_Mobile.Resources.Languages;
using EcoMeChan_Mobile.ViewModels;

namespace EcoMeChan_Mobile.Views
{
    public partial class AdminEditUserPage : ContentPage
    {
        public AdminEditUserPage(User user)
        {
            InitializeComponent();
            BindingContext = new AdminEditUserViewModel(user);
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();
            UpdateLocalization();
        }

        private void UpdateLocalization()
        {
            Title = AppResources.EditUserTitle;
            LoginLabel.Text = AppResources.LoginLabelTitle;
            EmailLabel.Text = AppResources.EmailLabelTitle;
            FirstNameLabel.Text = AppResources.FirstNameLabelTitle;
            LastNameLabel.Text = AppResources.LastNameLabelTitle;
            MiddleNameLabel.Text = AppResources.MiddleNameLabelTitle;
            PhoneLabel.Text = AppResources.PhoneLabelTitle;
            RoleLabel.Text = AppResources.RoleLabelTitle;
            SaveChangesButton.Text = AppResources.SaveChangesButtonText;

            var viewModel = (AdminEditUserViewModel)BindingContext;
            viewModel.LoginPlaceholder = AppResources.LoginPlaceholder;
            viewModel.EmailPlaceholder = AppResources.EmailPlaceholder;
            viewModel.FirstNamePlaceholder = AppResources.FirstNamePlaceholder;
            viewModel.LastNamePlaceholder = AppResources.LastNamePlaceholder;
            viewModel.MiddleNamePlaceholder = AppResources.MiddleNamePlaceholder;
            viewModel.PhonePlaceholder = AppResources.PhonePlaceholder;
        }
    }
}