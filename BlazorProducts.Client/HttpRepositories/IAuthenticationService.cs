using BlazorProducts.Entities.DataTransferObjects;
using Entities.DataTransferObjects;

namespace BlazorProducts.Client.HttpRepositories
{
    public interface IAuthenticationService
    {
        Task<UserRegistrationResponseDto> RegisterUserAsync(UserForRegistrationDto request);
    }
}