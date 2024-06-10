using EcoMeChan_Mobile.Resources.Languages;
using EcoMeChan_Mobile.Views;

namespace EcoMeChan_Mobile;

public partial class AppShell : Shell
{
    public AppShell()
    {
        InitializeComponent();
        UpdateLocalization();
    }

    private async void NavigateToLanguageSelection(object sender, EventArgs e)
    {
        await Navigation.PushAsync(new LanguageSelectionPage());
    }

    private void UpdateLocalization()
    {
        ConsumptionHistoryTab.Title = AppResources.ConsumptionHistoryTitle;
        IoTDevicesTab.Title = AppResources.IoTSensorsTitle;
        UserAccountTab.Title = AppResources.UserAccountTitle;
        UserManagementTab.Title = AppResources.UserManagementTitle;
        IoTDeviceManagementTab.Title = AppResources.IoTDeviceManagementTitle;
        MonitoringTab.Title = AppResources.MonitoringTitle;
        AdminAccountTab.Title = AppResources.AdminAccountTitle;
    }

    protected override void OnAppearing()
    {
        base.OnAppearing();
        UpdateLocalization();
    }
}