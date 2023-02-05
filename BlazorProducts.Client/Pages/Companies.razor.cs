using BlazorProducts.Client.Features;
using BlazorProducts.Client.HttpInterceptor;
using BlazorProducts.Client.HttpRepositories;
using Entities.DataTransferObjects;
using Microsoft.AspNetCore.Components;

namespace BlazorProducts.Client.Pages
{
    public partial class Companies : IDisposable
    {
        public IEnumerable<CompanyDto> CompaniesList { get; set; } = new List<CompanyDto>();
        public MetaData MetaData { get; set; } = new MetaData();
        private CompanyParameters _parameters = new CompanyParameters();

        [Inject]
        public ICompanyHttpRepository? CompanyRepository { get; set; }
        [Inject]
        public HttpInterceptorService? Interceptor { get; set; }

        protected async override Task OnInitializedAsync()
        {
            Interceptor?.RegisterEvent();
            await GetCompanies();
        }

        private async Task SelectedPage(int page)
        {
            _parameters.PageNumber = page;
            await GetCompanies();
        }

        private async Task GetCompanies()
        {
            var pagingResponse = await CompanyRepository?.GetCompaniesAsync(_parameters)!;
            CompaniesList = pagingResponse.Items!;
            MetaData = pagingResponse.MetaData!;
        }

        private async Task SetPageSize(int pageSize)
        {
            _parameters.PageSize = pageSize;
            _parameters.PageNumber = 1;

            await GetCompanies();
        }

        private async Task SetSearchTerm(string searchTerm)
        {
            _parameters.PageNumber = 1;
            _parameters.SearchTerm = searchTerm;

            await GetCompanies();
        }

        private async Task SetSortString(string sortBy)
        {
            _parameters.OrderBy = sortBy;
            await GetCompanies();
        }

        public void Dispose() => Interceptor?.DisposeEvent();
    }
}
