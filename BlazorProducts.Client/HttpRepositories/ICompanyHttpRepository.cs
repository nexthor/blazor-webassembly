using Entities.DataTransferObjects;
using Entities.Models;

namespace BlazorProducts.Client.HttpRepositories
{
    public interface ICompanyHttpRepository
    {
        Task<IEnumerable<CompanyDto>> GetCompaniesAsync();
        Task<CompanyDto> GetCompanyByIdAsync(Guid id);
    }
}