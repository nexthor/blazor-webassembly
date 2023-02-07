using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities.DataTransferObjects
{
    public class CompanyForCreationDto
    {
        [Required(ErrorMessage = "Name is required")]
        public string? Name { get; set; }
        public string? Address { get; set; }
        [Required(ErrorMessage = "Country is required")]
        [MaxLength(2, ErrorMessage = "Country only accept {1} characters")]
        public string? Country { get; set; }
        public IEnumerable<EmployeeForCreationDto>? Employees { get; set; }
    }
}
