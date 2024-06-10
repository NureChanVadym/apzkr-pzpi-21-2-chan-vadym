// Services/UserService.cs
using EcoMeChan_Mobile.Models;
using Newtonsoft.Json;
using System.Text;

namespace EcoMeChan_Mobile.Services
{
    public class UserService
    {
        private readonly HttpClient _httpClient;
        private readonly string _apiBaseUrl;

        public UserService()
        {
            _httpClient = new HttpClient();
            _apiBaseUrl = "https://525f-149-88-110-59.ngrok-free.app"; 
        }

        public async Task<List<User>> GetAllUsers()
        {
            var response = await _httpClient.GetAsync($"{_apiBaseUrl}/api/User");
            var responseContent = await response.Content.ReadAsStringAsync();

            if (response.IsSuccessStatusCode)
            {
                var users = JsonConvert.DeserializeObject<List<User>>(responseContent);
                return users;
            }
            else
            {
                return null;
            }
        }

        public async Task<bool> DeleteUser(int userId)
        {
            var response = await _httpClient.DeleteAsync($"{_apiBaseUrl}/api/User/{userId}");

            return response.IsSuccessStatusCode;
        }

        public async Task<bool> UpdateUser(int userId, User userData)
        {
            var json = JsonConvert.SerializeObject(userData);
            var content = new StringContent(json, System.Text.Encoding.UTF8, "application/json");
            var response = await _httpClient.PutAsync($"{_apiBaseUrl}/api/User/{userId}", content);

            if (response.IsSuccessStatusCode)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public async Task<User> CreateUser(User userData, string password)
        {

            var userWithPassword = new
            {
                login = userData.Login,
                password = password,
                phone = userData.Phone,
                email = userData.Email,
                lastName = userData.LastName,
                firstName = userData.FirstName,
                middleName = userData.MiddleName,
                role = (int)userData.Role
            };

            var json = JsonConvert.SerializeObject(userWithPassword);
            var content = new StringContent(json, Encoding.UTF8, "application/json");

            var response = await _httpClient.PostAsync($"{_apiBaseUrl}/api/User", content);
            var responseContent = await response.Content.ReadAsStringAsync();

            if (response.IsSuccessStatusCode)
            {
                var createdUser = JsonConvert.DeserializeObject<User>(responseContent);
                return createdUser;
            }
            else
            {
                return null;
            }
        }
    }
}