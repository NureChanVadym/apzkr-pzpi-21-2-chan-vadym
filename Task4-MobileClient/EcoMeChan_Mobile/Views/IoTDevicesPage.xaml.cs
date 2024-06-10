using EcoMeChan_Mobile.Resources.Languages;
using EcoMeChan_Mobile.ViewModels;

namespace EcoMeChan_Mobile.Views;

public partial class IoTDevicesPage : ContentPage
{
    private readonly IoTDevicesViewModel _viewModel;

    public IoTDevicesPage()
    {
        InitializeComponent();
        _viewModel = new IoTDevicesViewModel();
        BindingContext = _viewModel;
    }

    protected override void OnAppearing()
    {
        base.OnAppearing();
        _viewModel.LoadIoTDevicesCommand.Execute(null);
        UpdateLocalization();
    }

    private void UpdateLocalization()
    {
        Title = AppResources.IoTDevicesTitle;
        PageTitleLabel.Text = AppResources.IoTDevicesTitle;
    }

    private async void NavigateToLanguageSelection(object sender, EventArgs e)
    {
        await Navigation.PushAsync(new LanguageSelectionPage());
    }
}