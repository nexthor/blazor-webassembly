using BlazorProducts.Entities.DataTransferObjects;
using BlazorProducts.Entities.Models.Configurations;
using Entities.DataTransferObjects;
using Microsoft.AspNetCore.Components;
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

        public AuthenticationService(HttpClient httpClient)
        {
            _httpClient = httpClient;

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
    }
}
