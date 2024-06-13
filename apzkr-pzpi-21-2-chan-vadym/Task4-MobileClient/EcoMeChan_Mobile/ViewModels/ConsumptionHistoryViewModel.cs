using EcoMeChan_Mobile.Enums;
using EcoMeChan_Mobile.Models;
using EcoMeChan_Mobile.Resources.Languages;
using EcoMeChan_Mobile.Services;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Globalization;
using System.Windows.Input;

namespace EcoMeChan_Mobile.ViewModels
{
    public class ConsumptionHistoryViewModel : BaseViewModel
    {
        private readonly ConsumptionService _consumptionService;
        private readonly AuthService _authService;

        public ObservableCollection<Consumption> ConsumptionHistory { get; }
        public ICommand LoadConsumptionHistoryCommand { get; }

        public ConsumptionHistoryViewModel()
        {
            _consumptionService = new ConsumptionService();
            _authService = new AuthService();
            ConsumptionHistory = new ObservableCollection<Consumption>();
            LoadConsumptionHistoryCommand = new Command(async () => await LoadConsumptionHistory());
        }

        private async Task LoadConsumptionHistory()
        {
            var user = _authService.GetUser();
            if (user != null)
            {
                var history = await _consumptionService.GetUserConsumptionHistory(user.Id);
                ConsumptionHistory.Clear();

                foreach (var item in history)
                {
                    item.FormattedDate = FormatDate(item.Date);
                    item.FormattedTime = FormatTime(item.Date);
                    item.FormattedResourceType = GetLocalizedResourceType(item.ResourceType);
                    item.FormattedAmount = FormatAmount(item.ConsumedAmount, item.ResourceType, item.Unit);
                    item.FormattedCost = FormatCost(item.ConsumedCost, item.CurrencyCode);
                    ConsumptionHistory.Add(item);
                }
            }
        }

        public void UpdateLocalization()
        {
            foreach (var item in ConsumptionHistory)
            {
                item.FormattedDate = FormatDate(item.Date);
                item.FormattedTime = FormatTime(item.Date);
                item.FormattedResourceType = GetLocalizedResourceType(item.ResourceType);
                item.FormattedAmount = FormatAmount(item.ConsumedAmount, item.ResourceType, item.Unit);
                item.FormattedCost = FormatCost(item.ConsumedCost, item.CurrencyCode);
            }
        }

        private string FormatDate(DateTime date)
        {
            var culture = new CultureInfo(AppResources.Culture.Name);
            var format = culture.Name == "uk" ? "dd/MM/yyyy" : "MM/dd/yyyy";
            return date.ToString(format);
        }

        private string FormatTime(DateTime date)
        {
            var culture = new CultureInfo(AppResources.Culture.Name);
            var format = culture.Name == "uk" ? "HH:mm:ss" : "h:mm:ss tt";
            return date.ToString(format);
        }

        private string GetLocalizedResourceType(ResourceType resourceType)
        {
            switch (resourceType)
            {
                case ResourceType.Water:
                    return AppResources.ResourceTypeWater;
                case ResourceType.Gas:
                    return AppResources.ResourceTypeGas;
                case ResourceType.Electricity:
                    return AppResources.ResourceTypeElectricity;
                default:
                    return string.Empty;
            }
        }

        private string FormatAmount(decimal amount, ResourceType resourceType, string unitFrom)
        {
            var localizedUnit = GetLocalizedUnit(resourceType);
            var convertedAmount = ConvertAmount(amount, resourceType, unitFrom);
            return $"{convertedAmount:F2} {localizedUnit}";
        }

        private string FormatCost(decimal cost, string currencyCode)
        {
            var localizedCurrency = AppResources.Culture.Name == "uk" ? "UAH" : "USD";
            var convertedCost = ConvertCurrency(cost, currencyCode, localizedCurrency);
            return $"{convertedCost:F2} {GetLocalizedCurrency(localizedCurrency)}";
        }

        private string GetLocalizedUnit(ResourceType resourceType)
        {
            switch (resourceType)
            {
                case ResourceType.Water:
                    return AppResources.Culture.Name == "uk" ? "m³" : "gal";
                case ResourceType.Gas:
                    return AppResources.Culture.Name == "uk" ? "m³" : "ft³";
                case ResourceType.Electricity:
                    return "kWh";
                default:
                    return string.Empty;
            }
        }

        private double ConvertAmount(decimal amount, ResourceType resourceType, string unitFrom)
        {
            var localizedUnit = GetLocalizedUnit(resourceType);
            return ConvertUnit((double)amount, unitFrom, localizedUnit);
        }

        private double ConvertUnit(double value, string fromUnit, string toUnit)
        {
            if (fromUnit == toUnit)
            {
                return value;
            }

            switch (fromUnit)
            {
                case "m³":
                    if (toUnit == "l")
                        return value * 1000;
                    else if (toUnit == "gal")
                        return value * 264.172;
                    else if (toUnit == "ft³")
                        return value * 35.3147;
                    break;
                case "l":
                    if (toUnit == "m³")
                        return value / 1000;
                    else if (toUnit == "gal")
                        return value * 0.264172;
                    break;
                case "gal":
                    if (toUnit == "m³")
                        return value / 264.172;
                    else if (toUnit == "l")
                        return value / 0.264172;
                    break;
                case "ft³":
                    if (toUnit == "m³")
                        return value / 35.3147;
                    break;
                case "kWh":
                    if (toUnit == "MWh")
                        return value / 1000;
                    else if (toUnit == "J")
                        return value * 3.6e6;
                    break;
                case "MWh":
                    if (toUnit == "kWh")
                        return value * 1000;
                    else if (toUnit == "J")
                        return value * 3.6e9;
                    break;
                case "J":
                    if (toUnit == "kWh")
                        return value / 3.6e6;
                    else if (toUnit == "MWh")
                        return value / 3.6e9;
                    break;
                default:
                    return value;
            }

            return value;
        }

        private decimal ConvertCurrency(decimal cost, string fromCurrency, string toCurrency)
        {
            if (fromCurrency == toCurrency)
            {
                return cost;
            }

            var exchangeRates = GetExchangeRates();
            var exchangeRate = exchangeRates[$"{fromCurrency}_{toCurrency}"];
            return cost * (decimal)exchangeRate;
        }

        private string GetLocalizedCurrency(string currencyCode)
        {
            switch (currencyCode)
            {
                case "USD":
                    return AppResources.CurrencyUSD;
                case "EUR":
                    return AppResources.CurrencyEUR;
                case "UAH":
                    return AppResources.CurrencyUAH;
                default:
                    return currencyCode;
            }
        }

        private Dictionary<string, double> GetExchangeRates()
        {
            var exchangeRatesJson = Preferences.Get("ExchangeRates", null);
            if (string.IsNullOrEmpty(exchangeRatesJson))
            {
                return new Dictionary<string, double>();
            }

            return JsonConvert.DeserializeObject<Dictionary<string, double>>(exchangeRatesJson);
        }
    }
}