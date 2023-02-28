using Blazored.LocalStorage;
using BlazorProducts.Client.AuthProviders;
using BlazorProducts.Entities.DataTransferObjects;
using BlazorProducts.Entities.Models.Configurations;
using Entities.DataTransferObjects;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Authorization;
using System.Net.Http.Headers;
using System.Net.Http.Json;
using System.Text.Json;

namespace BlazorProducts.Client.HttpRepositories
{
    public class AuthenticationService : IAuthenticationService
    {
        private readonly HttpClient _httpClient;
        private readonly JsonSerializerOptions _options = new JsonSerializerOptions
        {
            PropertyNameCaseInsensitive = true,
        };
        private readonly AuthenticationStateProvider _authStateProvider;
        private readonly ILocalStorageService _localStorage;

        public AuthenticationService(HttpClient httpClient,
            AuthenticationStateProvider authStateProvider,
            ILocalStorageService localStorage)
        {
            _httpClient = httpClient;
            _authStateProvider = authStateProvider;
            _localStorage = localStorage;
        }

        public async Task<UserRegistrationResponseDto> RegisterUserAsync(UserForRegistrationDto request)
        {
            var roles = new List<string>
            {
                "Administrator",
            };
            request.Roles = roles;
            var response = await _httpClient.PostAsJsonAsync("authentication", request);

            if (!response.IsSuccessStatusCode)
            {
                var content = await response.Content.ReadAsStringAsync();
                var result = JsonSerializer.Deserialize<UserRegistrationResponseDto>(content, _options);

                return result ?? new UserRegistrationResponseDto { IsSuccessfulRegistration = false };
            }

            return new UserRegistrationResponseDto { IsSuccessfulRegistration = true };
        }

        public async Task<TokenDto> Login(UserForAuthenticationDto userForAuthentication)
        {
            var response = await _httpClient.PostAsJsonAsync("authentication/login",
                userForAuthentication);
            var content = await response.Content.ReadAsStringAsync();

            var result = JsonSerializer.Deserialize<TokenDto>(content, _options);

            if (!response.IsSuccessStatusCode)
                return result!;

            await _localStorage.SetItemAsync("authToken", result!.AccessToken);
            await _localStorage.SetItemAsync("refreshToken", result.RefreshToken);

            ((AuthStateProvider)_authStateProvider).NotifyUserAuthentication(
                result.AccessToken);

            _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue(
                "bearer", result.AccessToken);

            return new TokenDto(result!.AccessToken, result!.RefreshToken);
        }

        public async Task Logout()
        {
            await _localStorage.RemoveItemAsync("authToken");
            await _localStorage.RemoveItemAsync("refreshToken");

            ((AuthStateProvider)_authStateProvider).NotifyUserLogout();

            _httpClient.DefaultRequestHeaders.Authorization = null;
        }

        public async Task<string> RefreshToken()
        {
            var token = await _localStorage.GetItemAsync<string>("authToken");
            var refreshToken = await _localStorage.GetItemAsync<string>("refreshToken");

            var response = await _httpClient.PostAsJsonAsync("token/refresh",
                new TokenDto(token, refreshToken));

            var content = await response.Content.ReadAsStringAsync();
            var result = JsonSerializer.Deserialize<TokenDto>(content, _options);

            await _localStorage.SetItemAsync("authToken", result!.AccessToken);
            await _localStorage.SetItemAsync("refreshToken", result!.RefreshToken);

            _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue
                ("bearer", result.AccessToken);

            return result.AccessToken;
        }
    }
}
