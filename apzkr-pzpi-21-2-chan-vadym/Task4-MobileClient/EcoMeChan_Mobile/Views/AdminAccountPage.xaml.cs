using EcoMeChan_Mobile.Resources.Languages;
using EcoMeChan_Mobile.Services;
using Microsoft.Maui.Controls;

namespace EcoMeChan_Mobile.Views;

public partial class AdminAccountPage : ContentPage
{
    private readonly AuthService _authService;

    public AdminAccountPage()
    {
        InitializeComponent();
        _authService = new AuthService();
    }

    private void LoadUserData()
    {
        var user = _authService.GetUser();
        if (user != null)
        {
            LoginLabel.Text = user.Login;
            FirstNameLabel.Text = user.FirstName;
            LastNameLabel.Text = user.LastName;
            MiddleNameLabel.Text = user.MiddleName;
            PhoneLabel.Text = user.Phone;
            EmailLabel.Text = user.Email;
        }
    }

    protected override void OnAppearing()
    {
        base.OnAppearing();
        LoadUserData();
        UpdateLocalization();
    }

    private void UpdateLocalization()
    {
        PageTitleLabel.Text = AppResources.AdminAccountTitleText;
        PageTitleTextLabel.Text = AppResources.AdminAccountTitleText;
        LogoutButton.Text = AppResources.LogoutButtonText;
        LoginLabelTitle.Text = AppResources.LoginLabelTitle;
        FirstNameLabelTitle.Text = AppResources.FirstNameLabelTitle;
        LastNameLabelTitle.Text = AppResources.LastNameLabelTitle;
        MiddleNameLabelTitle.Text = AppResources.MiddleNameLabelTitle;
        PhoneLabelTitle.Text = AppResources.PhoneLabelTitle;
        EmailLabelTitle.Text = AppResources.EmailLabelTitle;
    }

    private async void LogoutButton_Clicked(object sender, EventArgs e)
    {
        await _authService.LogoutAsync();
        await Shell.Current.GoToAsync("//MainPage");
    }

    private async void NavigateToLanguageSelection(object sender, EventArgs e)
    {
        await Navigation.PushAsync(new LanguageSelectionPage());
    }
}