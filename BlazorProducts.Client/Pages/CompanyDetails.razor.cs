using BlazorProducts.Client.HttpRepositories;
using Entities.DataTransferObjects;
using Microsoft.AspNetCore.Components;

namespace BlazorProducts.Client.Pages
{
    public partial class CompanyDetails
    {
        [Inject]
        public ICompanyHttpRepository? CompanyRepo { get; set; }
        [Parameter]
        public Guid Id { get; set; }
        public CompanyDto Company { get; set; } = new CompanyDto();

        protected async override Task OnInitializedAsync()
        {
            Company = await CompanyRepo?.GetCompanyByIdAsync(Id)!;
        }

        private void AssignLogoUrl(string imgUrl)
        {

        }
    }
}
