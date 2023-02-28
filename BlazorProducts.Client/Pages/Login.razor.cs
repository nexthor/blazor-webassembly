using BlazorProducts.Client.HttpRepositories;
using Entities.DataTransferObjects;
using Microsoft.AspNetCore.Components;

namespace BlazorProducts.Client.Pages
{
	public partial class Login
	{
		private UserForAuthenticationDto _userForAuthentication =
			new UserForAuthenticationDto();

		[Inject]
		public IAuthenticationService? AuthenticationService { get; set; }

		[Inject]
		public NavigationManager? NavigationManager { get; set; }

		public bool ShowAuthError { get; set; }
		public string? Error { get; set; }

		public async Task ExecuteLogin()
		{
			ShowAuthError = false;

			var result = await AuthenticationService!.Login(_userForAuthentication);
			if (string.IsNullOrEmpty(result.AccessToken))
			{
				Error = "no token provided";
				ShowAuthError = true;
			}
			else
			{
				NavigationManager!.NavigateTo("/");
			}
		}
	}
}
