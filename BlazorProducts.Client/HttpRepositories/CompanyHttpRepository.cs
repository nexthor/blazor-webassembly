using Entities.DataTransferObjects;
using Entities.Models;
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

        public async Task<CompanyDto> GetCompanyByIdAsync(Guid id)
        {
            var company = await _httpClient.GetFromJsonAsync<CompanyDto>($"Companies/{id}");

            if (company is null) return new CompanyDto();

            return company;
        }
    }
}
