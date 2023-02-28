using BlazorProducts.Client.HttpRepositories;
using BlazorProducts.Entities.DataTransferObjects;
using Entities.DataTransferObjects;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Forms;

namespace BlazorProducts.Client.Pages
{
    public partial class Registration
    {
        public UserForRegistrationDto Form { get; set; } = new UserForRegistrationDto();
        [Inject]
        public IAuthenticationService? AuthenticationService { get; set; }
        [Inject]
        public NavigationManager? NavigationManager { get; set; }
        public bool ShowRegistrationErrors { get; set; } = false;
        public IEnumerable<string> Errors { get; set; } = new List<string>();

        public async Task Register()
        {
            var result = await AuthenticationService!.RegisterUserAsync(Form);

            if (!result.IsSuccessfulRegistration)
            {
                Errors = result.Errors!;
                ShowRegistrationErrors = true;
            }
            else
            {
                NavigationManager!.NavigateTo("/");
            }
        }
    }
}
