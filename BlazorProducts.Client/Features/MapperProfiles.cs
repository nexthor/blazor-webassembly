using AutoMapper;
using BlazorProducts.Entities.DataTransferObjects;
using Entities.Models;

namespace BlazorProducts.Client.Features
{
    public class MapperProfiles : Profile
    {
        public MapperProfiles()
        {
            CreateMap<CompanyDto, CompanyForUpdateDto>();
        }
    }
}
