using Blazored.LocalStorage;
using Blogs_Client.Model;
using Microsoft.AspNetCore.Components;
using System.Net.Http.Headers;
using System.Net.Http.Json;

namespace Blogs_Client.Services
{
    public class AuthService : IAuthService
    {
        private readonly HttpClient _httpClient;
        private readonly ILocalStorageService _localStorageService;
        private User? _user;
        public User? User 
        {
            get => _user;
            private set
            {
                if (_user != value)
                {
                    _user = value;
                    OnUserChanged?.Invoke();
                };
            } 
        }
        public AuthService(HttpClient httpClient, ILocalStorageService localStorageService)
        {
            _httpClient = httpClient;
            _localStorageService = localStorageService;
        }
        public event Action? OnUserChanged;

        public async Task Initialize()
        {
            User = await _localStorageService.GetItemAsync<User>("user");
        }
        public async Task Login(string username, string password)
        {
            try
            {
                var response = await _httpClient.PostAsJsonAsync("usersauth/login", new { username, password });
                if (response.IsSuccessStatusCode) 
                {
                    User user = await response.Content.ReadFromJsonAsync<User>() ?? throw new Exception("Failed to deserialize user data.");
                    User = user;
                    await _localStorageService.SetItemAsync("user", user);
                }
                else 
                {
                    var errMsg = await response.Content.ReadAsStringAsync();
                    Console.WriteLine(errMsg);
                    throw new Exception(errMsg);
                }
            }
            catch (HttpRequestException ex)
            {
                throw new Exception("Login failed: Unable to connect to the server.", ex);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task Logout()
        {
            User = null;
            await _localStorageService.RemoveItemAsync("user");
        }

        public async Task Register(string username, string email, string password)
        {
            try
            {
                var response = await _httpClient.PostAsJsonAsync("usersauth/register", new { username, email, password });
                if(!response.IsSuccessStatusCode)
                {
                    var errMsg = await response.Content.ReadAsStringAsync();
                    throw new Exception(errMsg);
                }
            }
            catch (HttpRequestException ex)
            { throw new Exception("Login failed: Unable to connect to the server.", ex); }
            catch (Exception ex) 
            {
                throw new Exception(ex.Message);
            }
        }
    }
}
