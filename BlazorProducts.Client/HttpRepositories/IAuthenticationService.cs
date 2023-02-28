using BlazorProducts.Entities.DataTransferObjects;
using Entities.DataTransferObjects;

namespace BlazorProducts.Client.HttpRepositories
{
    public interface IAuthenticationService
    {
        Task<UserRegistrationResponseDto> RegisterUserAsync(UserForRegistrationDto request);
        Task<TokenDto> Login(UserForAuthenticationDto userForAuthentication);
        Task Logout();
        Task<string> RefreshToken();
    }
}