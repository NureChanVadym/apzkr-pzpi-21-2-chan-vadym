using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace EcoMeChan_Mobile.Services
{
    public class CurrencyService
    {
        private readonly HttpClient _httpClient;
        private readonly string _exchangeBaseUrl;
        private readonly string _exchangeApiKey;

        public CurrencyService()
        {
            _httpClient = new HttpClient();
            _exchangeBaseUrl = "https://v6.exchangerate-api.com/v6";
            _exchangeApiKey = "dd639adaf7aba4899eab3d0e";
        }

        public async Task<Dictionary<string, double>> FetchExchangeRates()
        {
            try
            {
                var response = await _httpClient.GetAsync($"{_exchangeBaseUrl}/{_exchangeApiKey}/latest/UAH");
                var responseContent = await response.Content.ReadAsStringAsync();

                if (response.IsSuccessStatusCode)
                {
                    var data = JsonConvert.DeserializeObject<ExchangeRateResponse>(responseContent);
                    var rates = data.ConversionRates;

                    var exchangeRates = new Dictionary<string, double>
                    {
                        { "UAH_USD", rates.USD },
                        { "UAH_EUR", rates.EUR },
                        { "USD_EUR", rates.EUR / rates.USD },
                        { "USD_UAH", 1 / rates.USD },
                        { "EUR_UAH", 1 / rates.EUR },
                        { "EUR_USD", rates.USD / rates.EUR },
                        { "UAH_UAH", 1 },
                        { "USD_USD", 1 },
                        { "EUR_EUR", 1 }
                    };

                    return exchangeRates;
                }
                else
                {
                    // Handle error
                    return null;
                }
            }
            catch (Exception)
            {
                // Handle exception
                return null;
            }
        }

        private class ExchangeRateResponse
        {
            [JsonProperty("conversion_rates")]
            public ConversionRates ConversionRates { get; set; }
        }

        private class ConversionRates
        {
            public double USD { get; set; }
            public double EUR { get; set; }
        }
    }
}