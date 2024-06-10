using EcoMeChan_Mobile.Resources.Languages;
using EcoMeChan_Mobile.ViewModels;

namespace EcoMeChan_Mobile.Views;

public partial class RegistrationPage : ContentPage
{
    public RegistrationPage()
    {
        InitializeComponent();
        BindingContext = new RegistrationViewModel();
        UpdateLocalization();
    }

    protected override void OnAppearing()
    {
        base.OnAppearing();
        UpdateLocalization();
    }

    private void UpdateLocalization()
    {
        Title = AppResources.RegisterButtonText;
        LoginEntry.Placeholder = AppResources.LoginPlaceholder;
        PasswordEntry.Placeholder = AppResources.PasswordPlaceholder;
        PhoneEntry.Placeholder = AppResources.PhonePlaceholder;
        EmailEntry.Placeholder = AppResources.EmailPlaceholder;
        LastNameEntry.Placeholder = AppResources.LastNamePlaceholder;
        FirstNameEntry.Placeholder = AppResources.FirstNamePlaceholder;
        MiddleNameEntry.Placeholder = AppResources.MiddleNamePlaceholder;
        RegisterButton.Text = AppResources.RegisterButtonText;
    }
}