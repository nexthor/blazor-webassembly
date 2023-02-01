using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlazorProducts.Client.Features
{
    public class CompanyParameters : RequestParameters
    {
        public CompanyParameters() => OrderBy = "name";
    }
}
