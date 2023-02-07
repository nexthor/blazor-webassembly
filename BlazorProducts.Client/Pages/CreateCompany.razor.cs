using Blazored.Toast.Services;
using BlazorProducts.Client.HttpInterceptor;
using BlazorProducts.Client.HttpRepositories;
using Entities.DataTransferObjects;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Forms;

namespace BlazorProducts.Client.Pages
{
    public partial class CreateCompany : IDisposable
    {
        private CompanyForCreationDto _companyForm = new CompanyForCreationDto();
        private EditContext? _editContext;
        private bool formInvalid = true;
        [Inject]
        public ICompanyHttpRepository? Repository { get; set; }
        [Inject]
        public HttpInterceptorService? Interceptor { get; set; }
        [Inject]
        public IToastService? ToastService { get; set; }

        protected override void OnInitialized()
        {
            _editContext = new EditContext(_companyForm);
            _editContext.OnFieldChanged += HandleFieldChanged;
            Interceptor?.RegisterEvent();
        }

        private void HandleFieldChanged(object? sender, FieldChangedEventArgs e)
        {
            formInvalid = (bool)!_editContext?.Validate()!;
            StateHasChanged();
        }

        private async Task Create()
        {
            var company = await Repository?.CreateCompany(_companyForm)!;
            // showing a Toast when company is created
            ToastService?.ShowSuccess($"Company {company.Name} created successfully!");
            // reset the form
            _companyForm = new CompanyForCreationDto();
            formInvalid = true;
        }

        public void Dispose() => Interceptor?.DisposeEvent();
    }
}
