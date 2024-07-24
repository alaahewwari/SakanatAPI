using AutoMapper;
using BusinessLogic.DTOs.UserDtos.Requests;
using BusinessLogic.DTOs.UserDtos.Responses;
using DataAccess.Models;

namespace BusinessLogic.MappingProfiles
{
    public class SuspensionMappings : Profile
    {
        public SuspensionMappings()
        {
            CreateMap<Suspension, GetSuspendedUserResponseDto>().ReverseMap();

            CreateMap<Suspension, SuspendUserRequestDto>().ReverseMap();
        }
    }
}
