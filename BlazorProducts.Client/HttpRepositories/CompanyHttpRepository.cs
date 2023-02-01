using BlazorProducts.Client.Features;
using Entities.DataTransferObjects;
using Entities.Models;
using Microsoft.AspNetCore.WebUtilities;
using System.Net.Http.Json;
using System.Text.Json;

namespace BlazorProducts.Client.HttpRepositories
{
    public class CompanyHttpRepository : ICompanyHttpRepository
    {
        private readonly HttpClient _httpClient;
        private readonly JsonSerializerOptions _options = new JsonSerializerOptions
        {
            PropertyNameCaseInsensitive= true,
        };

        public CompanyHttpRepository(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<PagingResponse<CompanyDto>> GetCompaniesAsync(CompanyParameters companyParameters)
        {
            var queryStringParam = new Dictionary<string, string> 
            {
                ["pageNumber"] = companyParameters.PageNumber.ToString(),
                ["pageSize"] = companyParameters.PageSize.ToString(),
            };
            var response = await _httpClient
                                    .GetAsync(QueryHelpers.AddQueryString("Companies", queryStringParam));
            var content = await response.Content.ReadAsStringAsync();

            if(!response.IsSuccessStatusCode)
            {
                throw new ApplicationException(content);
            }

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
    }
}
