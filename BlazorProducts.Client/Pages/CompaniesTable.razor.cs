using Entities.DataTransferObjects;
using Microsoft.AspNetCore.Components;

namespace BlazorProducts.Client.Pages
{
    public partial class CompaniesTable
    {
        [Parameter]
        public IEnumerable<CompanyDto> Companies { get; set; } = new List<CompanyDto>();
    }
}
