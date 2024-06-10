using EcoMeChan_Mobile.Models;
using EcoMeChan_Mobile.Resources.Languages;
using EcoMeChan_Mobile.Services;

namespace EcoMeChan_Mobile.Views;

public partial class EditUserPage : ContentPage
{
    private readonly AuthService _authService;
    private User _user;
    public Command BackCommand { get; }

    public EditUserPage()
    {
        InitializeComponent();
        _authService = new AuthService();
        BackCommand = new Command(async () => await GoBack());
        BindingContext = this;
    }

    protected override void OnAppearing()
    {
        base.OnAppearing();
        LoadUserData();
        UpdateLocalization();
    }

    private async Task GoBack()
    {
        await Shell.Current.GoToAsync("..");
    }

    private void LoadUserData()
    {
        _user = _authService.GetUser();
        if (_user != null)
        {
            FirstNameEntry.Text = _user.FirstName;
            LastNameEntry.Text = _user.LastName;
            MiddleNameEntry.Text = _user.MiddleName;
            PhoneEntry.Text = _user.Phone;
            EmailEntry.Text = _user.Email;
        }
    }

    private void UpdateLocalization()
    {
        Title = AppResources.EditUserTitle;
        PageTitleLabel.Text = AppResources.EditUserTitle;
        FirstNameLabelTitle.Text = AppResources.FirstNameLabelTitle;
        LastNameLabelTitle.Text = AppResources.LastNameLabelTitle;
        MiddleNameLabelTitle.Text = AppResources.MiddleNameLabelTitle;
        PhoneLabelTitle.Text = AppResources.PhoneLabelTitle;
        EmailLabelTitle.Text = AppResources.EmailLabelTitle;
        SaveChangesButton.Text = AppResources.SaveChangesButtonText;
        GoBackButton.Text = AppResources.GoBackButtonText;
    }

    private async void SaveChangesButton_Clicked(object sender, EventArgs e)
    {
        _user.FirstName = FirstNameEntry.Text;
        _user.LastName = LastNameEntry.Text;
        _user.MiddleName = MiddleNameEntry.Text;
        _user.Phone = PhoneEntry.Text;
        _user.Email = EmailEntry.Text;

        var isUpdateSuccessful = await _authService.UpdateUserAsync(_user.Id, _user);
        if (isUpdateSuccessful)
        {
            await DisplayAlert(AppResources.SuccessTitle, AppResources.AccountUpdatedSuccessMessage, "OK");
            _authService.SaveUser(_user);
            await Shell.Current.GoToAsync("//UserTab/UserAccountPage");
        }
        else
        {
            await DisplayAlert(AppResources.ErrorTitle, AppResources.AccountUpdateErrorMessage, "OK");
        }
    }

    private async void GoBackButton_Clicked(object sender, EventArgs e)
    {
        await Shell.Current.GoToAsync("//UserTab/UserAccountPage");
    }
}