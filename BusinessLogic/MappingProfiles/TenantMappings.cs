using AutoMapper;
using BusinessLogic.DTOs.TenantsDtos.Requests;
using BusinessLogic.DTOs.TenantsDtos.Responses;
using DataAccess.Models;
namespace BusinessLogic.MappingProfiles
{
    public class TenantMappings : Profile
    {
        public TenantMappings()
        {
            CreateMap<Tenant, TenantRequestDto>().ReverseMap();
            CreateMap<Tenant, TenantResponseDto>().ReverseMap();
        }
    }
}
