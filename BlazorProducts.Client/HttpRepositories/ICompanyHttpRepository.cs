using BlazorProducts.Client.Features;
using BlazorProducts.Entities.DataTransferObjects;
using Entities.DataTransferObjects;
using Entities.Models;

namespace BlazorProducts.Client.HttpRepositories
{
    public interface ICompanyHttpRepository
    {
        Task<PagingResponse<CompanyDto>> GetCompaniesAsync(CompanyParameters companyParameters);
        Task<CompanyDto> GetCompanyByIdAsync(Guid id);
        Task<CompanyDto> CreateCompany(CompanyForCreationDto request);
        Task<string> UploadCompanyLogo(Guid id, MultipartFormDataContent content);
        Task UpdateCompany(Guid id, CompanyForUpdateDto dto);
        Task DeleteCompany(Guid id);
    }
}