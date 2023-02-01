using BlazorProducts.Client.Features;
using Entities.DataTransferObjects;
using Entities.Models;

namespace BlazorProducts.Client.HttpRepositories
{
    public interface ICompanyHttpRepository
    {
        Task<PagingResponse<CompanyDto>> GetCompaniesAsync(CompanyParameters companyParameters);
        Task<CompanyDto> GetCompanyByIdAsync(Guid id);
    }
}