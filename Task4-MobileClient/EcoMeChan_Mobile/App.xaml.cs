using EcoMeChan_Mobile.Enums;
using EcoMeChan_Mobile.Resources.Languages;
using EcoMeChan_Mobile.Services;
using System.Globalization;
using Newtonsoft.Json;

namespace EcoMeChan_Mobile;

public partial class App : Application
{
    public App()
    {
        InitializeComponent();

        var culture = new CultureInfo("en");
        AppResources.Culture = culture;

        var authService = new AuthService();
        var savedUser = authService.GetUser();

        if (savedUser != null)
        {
            if (savedUser.Role == Role.User)
            {
                MainPage = new AppShell();
                Shell.Current.GoToAsync("//UserTab/UserAccountPage");
            }
            else if (savedUser.Role == Role.Admin)
            {
                MainPage = new AppShell();
                Shell.Current.GoToAsync("//AdminTab/AdminAccountPage");
            }
        }
        else
        {
            MainPage = new AppShell();
        }

        FetchAndSaveExchangeRates();
    }

    private async void FetchAndSaveExchangeRates()
    {
        var currencyService = new CurrencyService();
        var exchangeRates = await currencyService.FetchExchangeRates();

        if (exchangeRates != null)
        {
            var exchangeRatesJson = JsonConvert.SerializeObject(exchangeRates);
            Preferences.Set("ExchangeRates", exchangeRatesJson);
        }
    }
}