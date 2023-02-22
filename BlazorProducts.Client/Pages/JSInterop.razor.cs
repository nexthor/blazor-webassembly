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

        protected override async Task OnInitializedAsync()
        {
            _jsModule = await JSRuntime!.InvokeAsync<IJSObjectReference>("import", "./js/jsExamples.js");
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
    }
}
