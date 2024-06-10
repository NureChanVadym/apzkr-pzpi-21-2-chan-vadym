// Services/IoTDeviceService.cs
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using EcoMeChan_Mobile.Models;
using Newtonsoft.Json;

namespace EcoMeChan_Mobile.Services
{
    public class IoTDeviceService
    {
        private readonly HttpClient _httpClient;
        private readonly string _apiBaseUrl;

        public IoTDeviceService()
        {
            _httpClient = new HttpClient();
            _apiBaseUrl = "https://525f-149-88-110-59.ngrok-free.app"; 
        }

        public async Task<List<IoTDevice>> GetUserIoTDevices(int userId)
        {
            var response = await _httpClient.GetAsync($"{_apiBaseUrl}/api/IoTDevice/user/{userId}");
            var responseContent = await response.Content.ReadAsStringAsync();

            if (response.IsSuccessStatusCode)
            {
                var devices = JsonConvert.DeserializeObject<List<IoTDevice>>(responseContent);
                return devices;
            }
            else
            {
                // Обробка помилки отримання даних
                return null;
            }
        }

        public async Task<List<IoTDevice>> GetAllIoTDevicesAsync()
        {
            var response = await _httpClient.GetAsync($"{_apiBaseUrl}/api/IoTDevice");
            var responseContent = await response.Content.ReadAsStringAsync();

            if (response.IsSuccessStatusCode)
            {
                var devices = JsonConvert.DeserializeObject<List<IoTDevice>>(responseContent);
                return devices;
            }
            else
            {
                // Обробка помилки
                return null;
            }
        }

        public async Task<bool> UpdateIoTDeviceAsync(int deviceId, IoTDevice deviceData)
        {
            var json = JsonConvert.SerializeObject(deviceData);
            var content = new StringContent(json, Encoding.UTF8, "application/json");

            var response = await _httpClient.PutAsync($"{_apiBaseUrl}/api/IoTDevice/{deviceId}", content);

            return response.IsSuccessStatusCode;
        }

        public async Task<bool> DeleteIoTDeviceAsync(int deviceId)
        {
            var response = await _httpClient.DeleteAsync($"{_apiBaseUrl}/api/IoTDevice/{deviceId}");

            return response.IsSuccessStatusCode;
        }
    }
}