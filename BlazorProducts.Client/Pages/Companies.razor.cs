using BlazorProducts.Client.HttpRepositories;
using Entities.DataTransferObjects;
using Microsoft.AspNetCore.Components;

namespace BlazorProducts.Client.Pages
{
    public partial class Companies
    {
        public IEnumerable<CompanyDto> CompaniesList { get; set; } = new List<CompanyDto>();
        [Inject]
        public ICompanyHttpRepository? CompanyRepository { get; set; }

        protected async override Task OnInitializedAsync()
        {
            CompaniesList = await CompanyRepository?.GetCompaniesAsync()!;

            foreach(var company in CompaniesList)
            {
                Console.WriteLine($"Company: {company.Name}");
            }
        }
    }
}
