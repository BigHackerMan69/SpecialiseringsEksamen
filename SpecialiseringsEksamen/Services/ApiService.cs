using System;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace SpecialiseringsEksamen.Services
{
    public class ApiService
    {
        private readonly HttpClient _httpClient;

        public ApiService()
        {
            _httpClient = new HttpClient
            {
                BaseAddress = new Uri("http://localhost:3001/api/") 
            };
            _httpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
        }

        public async Task<(bool isSuccess, string message)> RegisterUserAsync(string username, string password)
        {
            var data = new { username, password };
            var content = new StringContent(JsonConvert.SerializeObject(data), Encoding.UTF8, "application/json");
            var response = await _httpClient.PostAsync("auth/register", content);

            if (response.IsSuccessStatusCode)
            {
                return (true, "User registered successfully");
            }
            else
            {
                var message = await response.Content.ReadAsStringAsync();
                return (false, message);
            }
        }

        public async Task<(bool isSuccess, string message)> LoginUserAsync(string username, string password)
        {
            var data = new { username, password };
            var content = new StringContent(JsonConvert.SerializeObject(data), Encoding.UTF8, "application/json");
            var response = await _httpClient.PostAsync("auth/login", content);

            if (response.IsSuccessStatusCode)
            {
                return (true, "Login successful");
            }
            else
            {
                var message = await response.Content.ReadAsStringAsync();
                return (false, message);
            }
        }

        public async Task<(bool isSuccess, string message)> UnlockLockerAsync(string lockerNumber)
        {
            var data = new { action = "on", lockerNumber };
            var content = new StringContent(JsonConvert.SerializeObject(data), Encoding.UTF8, "application/json");
            var response = await _httpClient.PostAsync("drawer/unlock", content);

            if (response.IsSuccessStatusCode)
            {
                return (true, "Locker unlocked successfully");
            }
            else
            {
                var message = await response.Content.ReadAsStringAsync();
                return (false, message);
            }
        }

        public async Task<(bool isSuccess, string message)> LockLockerAsync(string lockerNumber)
        {
            var data = new { action = "off", lockerNumber };
            var content = new StringContent(JsonConvert.SerializeObject(data), Encoding.UTF8, "application/json");
            var response = await _httpClient.PostAsync("drawer/lock", content);

            if (response.IsSuccessStatusCode)
            {
                return (true, "Locker locked successfully");
            }
            else
            {
                var message = await response.Content.ReadAsStringAsync();
                return (false, message);
            }
        }
    }
}
