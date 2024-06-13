using EcoMeChan_Mobile.Models;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;

namespace EcoMeChan_Mobile.Services
{
    public class ConsumptionService
    {
        private readonly HttpClient _httpClient;
        private readonly string _apiBaseUrl;

        public ConsumptionService()
        {
            _httpClient = new HttpClient();
            _apiBaseUrl = "https://525f-149-88-110-59.ngrok-free.app";
        }

        public async Task<List<Consumption>> GetUserConsumptionHistory(int userId)
        {
            var response = await _httpClient.GetAsync($"{_apiBaseUrl}/api/Consumption/user/{userId}");
            var responseContent = await response.Content.ReadAsStringAsync();

            if (response.IsSuccessStatusCode)
            {
                var history = JsonConvert.DeserializeObject<List<Consumption>>(responseContent);
                return history;
            }
            else
            {
                // Handle error
                return null;
            }
        }
    }
}