using Entities.DataTransferObjects;

namespace BlazorProducts.Client.HttpRepositories
{
    public interface ICompanyHttpRepository
    {
        Task<IEnumerable<CompanyDto>> GetCompaniesAsync();
    }
}