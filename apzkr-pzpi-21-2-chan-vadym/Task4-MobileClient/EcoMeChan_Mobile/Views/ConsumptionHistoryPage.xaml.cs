using EcoMeChan_Mobile.Resources.Languages;
using EcoMeChan_Mobile.ViewModels;

namespace EcoMeChan_Mobile.Views;

public partial class ConsumptionHistoryPage : ContentPage
{
    private readonly ConsumptionHistoryViewModel _viewModel;

    public ConsumptionHistoryPage()
    {
        InitializeComponent();
        _viewModel = new ConsumptionHistoryViewModel();
        BindingContext = _viewModel;
    }

    protected override void OnAppearing()
    {
        base.OnAppearing();
        _viewModel.LoadConsumptionHistoryCommand.Execute(null);
        UpdateLocalization();
    }

    private void UpdateLocalization()
    {
        PageTitleLabel.Text = AppResources.ConsumptionHistoryTitle;
        PageTitleTextLabel.Text = AppResources.ConsumptionHistoryTitle;
        DateLabelTitle.Text = AppResources.DateLabel;
        TimeLabelTitle.Text = AppResources.TimeLabel;
        ResourceTypeLabelTitle.Text = AppResources.ResourceTypeLabel;
        AmountLabelTitle.Text = AppResources.AmountLabel;
        CostLabelTitle.Text = AppResources.CostLabel;
        _viewModel.UpdateLocalization();
    }

    private async void NavigateToLanguageSelection(object sender, EventArgs e)
    {
        await Navigation.PushAsync(new LanguageSelectionPage());
    }
}