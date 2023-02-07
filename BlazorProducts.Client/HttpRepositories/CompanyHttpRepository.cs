using BlazorProducts.Client.Features;
using Entities.DataTransferObjects;
using Entities.Models;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.WebUtilities;
using System.Net;
using System.Net.Http.Json;
using System.Text.Json;

namespace BlazorProducts.Client.HttpRepositories
{
    public class CompanyHttpRepository : ICompanyHttpRepository
    {
        private readonly HttpClient _httpClient;
        private readonly NavigationManager _navigationManager;
        private readonly JsonSerializerOptions _options = new JsonSerializerOptions
        {
            PropertyNameCaseInsensitive= true,
        };

        public CompanyHttpRepository(HttpClient httpClient, NavigationManager navigationManager)
        {
            _httpClient = httpClient;
            _navigationManager = navigationManager;
        }

        public async Task<PagingResponse<CompanyDto>> GetCompaniesAsync(CompanyParameters companyParameters)
        {
            var queryStringParam = new Dictionary<string, string> 
            {
                ["pageNumber"] = companyParameters.PageNumber.ToString(),
                ["pageSize"] = companyParameters.PageSize.ToString(),
            };

            if (!string.IsNullOrEmpty(companyParameters.SearchTerm))
                queryStringParam.Add("searchTerm", companyParameters.SearchTerm);

            if (!string.IsNullOrEmpty(companyParameters.OrderBy))
                queryStringParam.Add("orderBy", companyParameters.OrderBy);

            var response = await _httpClient
                                    .GetAsync(QueryHelpers.AddQueryString("Companies", queryStringParam));
            var content = await response.Content.ReadAsStringAsync();

            var pagingResponse = new PagingResponse<CompanyDto> 
            {
                Items = JsonSerializer.Deserialize<List<CompanyDto>>(content, _options),
                MetaData = JsonSerializer.Deserialize<MetaData>(response.Headers.GetValues("X-Pagination").First(), _options)
            };

            return pagingResponse;
        }

        public async Task<CompanyDto> GetCompanyByIdAsync(Guid id)
        {
            var company = await _httpClient.GetFromJsonAsync<CompanyDto>($"Companies/{id}");

            if (company is null) return new CompanyDto();

            return company;
        }

        public async Task<CompanyDto> CreateCompany(CompanyForCreationDto request)
        {
            var company = await _httpClient.PostAsJsonAsync("companies", request);
            var response = JsonSerializer.Deserialize<CompanyDto>(company.Content.ReadAsStream());

            return response ?? new CompanyDto();
        }
    }
}
