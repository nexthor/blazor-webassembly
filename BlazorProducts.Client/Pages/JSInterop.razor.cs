using Microsoft.AspNetCore.Components;
using Microsoft.JSInterop;

namespace BlazorProducts.Client.Pages
{
    public partial class JSInterop
    {
        [Inject]
        public IJSRuntime? JSRuntime { get; set; }

        private IJSObjectReference? _jsModule;
        private string? _registrationResult;
        private string? _emailDetails;
        private ElementReference? _inputRef;
        private string? _errorMessage;

        protected override async Task OnAfterRenderAsync(bool firstRender)
        {
            if (firstRender)
            {
                _jsModule = await JSRuntime!.InvokeAsync<IJSObjectReference>("import", "./js/jsExamples.js");

                await ThrowError();
            }
        }

        public async Task ShowAlertWindow()
        {
            //await _jsModule!.InvokeVoidAsync("showAlert", "JS Function call");
            await _jsModule!.InvokeVoidAsync("showAlertObject", new { Name = "Nestor", Age = 45 });
        }

        private async Task RegisterEmail()
        {
            _registrationResult = await _jsModule!.InvokeAsync<string>("emailRegistration", "Please provide your email");
        }

        private async Task GetEmailDetails()
        {
            var email = await _jsModule!.InvokeAsync<EmailDetail>("getEmailDetails", "please provide a valid email");

            if (email != null)
                _emailDetails = $"name: {email.Name}, server: {email.Server}, domain: {email.Domain}";
        }

        private async Task FocusAndStyleElement()
        {
            await _jsModule!.InvokeVoidAsync("focusAndStyleElement", _inputRef);
        }

        private async Task ThrowError()
        {
            try
            {
                await _jsModule!.InvokeVoidAsync("throwError");
            } 
            catch (JSException ex)
            {
                _errorMessage = ex.Message;
                StateHasChanged();
            }
        }
    }

    class EmailDetail
    {
        public string? Name { get; set; }
        public string? Server { get; set; }
        public string? Domain { get; set; }
    }
}
