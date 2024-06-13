using EcoMeChan_Mobile.Resources.Languages;
using EcoMeChan_Mobile.Views;

namespace EcoMeChan_Mobile;

public partial class MainPage : ContentPage
{
    public MainPage()
    {
        InitializeComponent();
        UpdateLocalization();
    }

    private async void RegisterButton_Clicked(object sender, EventArgs e)
    {
        await Navigation.PushAsync(new RegistrationPage());
    }

    private async void LoginButton_Clicked(object sender, EventArgs e)
    {
        await Navigation.PushAsync(new LoginPage());
    }

    protected override void OnAppearing()
    {
        base.OnAppearing();
        UpdateLocalization();
    }

    private void UpdateLocalization()
    {
        WelcomeLabel.Text = AppResources.WelcomeText;
        RegisterButton.Text = AppResources.RegisterButtonText;
        LoginButton.Text = AppResources.LoginButtonText;
    }
}