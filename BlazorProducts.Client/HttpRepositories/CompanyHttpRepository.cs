using Entities.DataTransferObjects;
using System.Net.Http.Json;

namespace BlazorProducts.Client.HttpRepositories
{
    public class CompanyHttpRepository : ICompanyHttpRepository
    {
        private readonly HttpClient _httpClient;

        public CompanyHttpRepository(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<IEnumerable<CompanyDto>> GetCompaniesAsync()
        {
            var companies = await _httpClient.GetFromJsonAsync<IEnumerable<CompanyDto>>("Companies");

            if (companies is null) return new List<CompanyDto>();

            return companies;
        }
    }
}
