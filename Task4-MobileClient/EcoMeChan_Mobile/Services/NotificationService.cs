// Services/NotificationService.cs
using EcoMeChan_Mobile.Models;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;

namespace EcoMeChan_Mobile.Services
{
    public class NotificationService
    {
        private readonly HttpClient _httpClient;
        private readonly string _apiBaseUrl;

        public NotificationService()
        {
            _httpClient = new HttpClient();
            _apiBaseUrl = "https://525f-149-88-110-59.ngrok-free.app";
        }

        public async Task<List<Notification>> GetAllNotifications()
        {
            var response = await _httpClient.GetAsync($"{_apiBaseUrl}/api/Notification");
            var responseContent = await response.Content.ReadAsStringAsync();

            if (response.IsSuccessStatusCode)
            {
                var notifications = JsonConvert.DeserializeObject<List<Notification>>(responseContent);
                return notifications;
            }
            else
            {
                // Обробка помилки отримання даних
                return null;
            }
        }
    }
}