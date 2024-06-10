using EcoMeChan_Mobile.Resources.Languages;
using EcoMeChan_Mobile.ViewModels;

namespace EcoMeChan_Mobile.Views;

public partial class IoTDeviceManagementPage : ContentPage
{
    public IoTDeviceManagementPage()
    {
        InitializeComponent();
        BindingContext = new IoTDeviceManagementViewModel();
    }

    protected override void OnAppearing()
    {
        base.OnAppearing();
        UpdateLocalization();
    }

    private void UpdateLocalization()
    {
        Title = AppResources.IoTDeviceManagementTitle;
        PageTitleLabel.Text = AppResources.IoTDeviceManagementTitle;
        PageTitleTextLabel.Text = AppResources.IoTDeviceManagementTitle;
    }

    private async void NavigateToLanguageSelection(object sender, EventArgs e)
    {
        await Navigation.PushAsync(new LanguageSelectionPage());
    }
}