using Blazored.Toast.Services;
using BlazorProducts.Client.HttpInterceptor;
using BlazorProducts.Client.HttpRepositories;
using Entities.DataTransferObjects;
using Microsoft.AspNetCore.Components.Forms;
using Microsoft.AspNetCore.Components;
using BlazorProducts.Entities.DataTransferObjects;
using AutoMapper;

namespace BlazorProducts.Client.Pages
{
    public partial class UpdateCompany
    {
#pragma warning disable CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.
        private CompanyForUpdateDto _companyForm;
#pragma warning restore CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.
        private EditContext? _editContext;
        private bool formInvalid = true;
        [Parameter]
        public Guid Id { get; set; }
        [Inject]
        public ICompanyHttpRepository? Repository { get; set; }
        [Inject]
        public HttpInterceptorService? Interceptor { get; set; }
        [Inject]
        public IToastService? ToastService { get; set; }
        [Inject]
        public IMapper? Mapper { get; set; }

        protected override async Task OnInitializedAsync()
        {
            var company = await Repository?.GetCompanyByIdAsync(Id)!;
            _companyForm = Mapper!.Map<CompanyForUpdateDto>(company);

            _editContext = new EditContext(_companyForm);
            _editContext.OnFieldChanged += HandleFieldChanged;
            Interceptor?.RegisterEvent();
        }

        private void HandleFieldChanged(object? sender, FieldChangedEventArgs e)
        {
            formInvalid = (bool)!_editContext?.Validate()!;
            StateHasChanged();
        }

        private async Task Update()
        {
            await Repository?.UpdateCompany(Id, _companyForm!)!;
            // showing a Toast when company is created
            ToastService?.ShowSuccess($"Company {Id} created successfully!");
            // reset the form
            formInvalid = true;
        }

        public void Dispose() => Interceptor?.DisposeEvent();
    }
}
