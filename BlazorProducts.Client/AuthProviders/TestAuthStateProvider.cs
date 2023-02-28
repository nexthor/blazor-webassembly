using Microsoft.AspNetCore.Components.Authorization;
using System.Security.Claims;

namespace BlazorProducts.Client.AuthProviders
{
    public class TestAuthStateProvider : AuthenticationStateProvider
    {
        public async override Task<AuthenticationState> GetAuthenticationStateAsync()
        {
            await Task.Delay(1500);
            var claims = new List<Claim>
            {
                new Claim(ClaimTypes.Name, "Nestor Mendoza"),
                new Claim(ClaimTypes.Role, "Administrator")
            };

            return await Task.FromResult(new AuthenticationState(new ClaimsPrincipal()));
        }
    }
}
