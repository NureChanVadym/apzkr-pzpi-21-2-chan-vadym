// Views/MonitoringPage.xaml.cs
using EcoMeChan_Mobile.Resources.Languages;
using EcoMeChan_Mobile.ViewModels;

namespace EcoMeChan_Mobile.Views
{
    public partial class MonitoringPage : ContentPage
    {
        private readonly MonitoringViewModel _viewModel;

        public MonitoringPage()
        {
            InitializeComponent();
            _viewModel = new MonitoringViewModel();
            BindingContext = _viewModel;
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();
            _viewModel.LoadNotificationsCommand.Execute(null);
            UpdateLocalization();
        }

        private void UpdateLocalization()
        {
            PageTitleLabel.Text = AppResources.MonitoringTitle;
            PageTitleTextLabel.Text = AppResources.MonitoringTitle;
            NotificationIdLabelTitle.Text = AppResources.NotificationIdLabel;
            NotificationIoTDeviceLabelTitle.Text = AppResources.NotificationIoTDeviceLabel;
            NotificationTypeLabelTitle.Text = AppResources.NotificationTypeLabel;
            NotificationTextLabelTitle.Text = AppResources.NotificationTextLabel;
            NotificationCreatedAtLabelTitle.Text = AppResources.NotificationCreatedAtLabel;
            _viewModel.UpdateDateTimeFormat();
        }

        private async void NavigateToLanguageSelection(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new LanguageSelectionPage());
        }
    }
}