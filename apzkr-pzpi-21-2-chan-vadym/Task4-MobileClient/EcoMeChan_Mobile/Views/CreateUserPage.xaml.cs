using EcoMeChan_Mobile.Resources.Languages;
using EcoMeChan_Mobile.ViewModels;

namespace EcoMeChan_Mobile.Views
{
    public partial class CreateUserPage : ContentPage
    {
        public CreateUserPage(AdminAccountManagementViewModel adminViewModel)
        {
            InitializeComponent();
            var viewModel = new CreateUserViewModel();
            viewModel.SetAdminViewModel(adminViewModel);
            BindingContext = viewModel;
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();
            UpdateLocalization();
        }

        private void UpdateLocalization()
        {
            Title = AppResources.CreateUserTitle;
            LoginLabel.Text = AppResources.LoginLabelTitle;
            EmailLabel.Text = AppResources.EmailLabelTitle;
            FirstNameLabel.Text = AppResources.FirstNameLabelTitle;
            LastNameLabel.Text = AppResources.LastNameLabelTitle;
            MiddleNameLabel.Text = AppResources.MiddleNameLabelTitle;
            PhoneLabel.Text = AppResources.PhoneLabelTitle;
            PasswordLabel.Text = AppResources.PasswordLabelTitle;
            RoleLabel.Text = AppResources.RoleLabelTitle;
            CreateUserButton.Text = AppResources.CreateUserButtonText;

            var viewModel = (CreateUserViewModel)BindingContext;
            viewModel.LoginPlaceholder = AppResources.LoginPlaceholder;
            viewModel.EmailPlaceholder = AppResources.EmailPlaceholder;
            viewModel.FirstNamePlaceholder = AppResources.FirstNamePlaceholder;
            viewModel.LastNamePlaceholder = AppResources.LastNamePlaceholder;
            viewModel.MiddleNamePlaceholder = AppResources.MiddleNamePlaceholder;
            viewModel.PhonePlaceholder = AppResources.PhonePlaceholder;
            viewModel.PasswordPlaceholder = AppResources.PasswordPlaceholder;
            viewModel.RolePickerTitle = AppResources.RolePickerTitle;
        }
    }
}