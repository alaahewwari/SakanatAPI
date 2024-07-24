using AutoMapper;
using BusinessLogic.DTOs.AuthenticationDtos.Requests;
using BusinessLogic.DTOs.UserDtos.Responses;
using DataAccess.Models;


namespace BusinessLogic.MappingProfiles
{
    public class ApplicationUserMappings : Profile
    {
        public ApplicationUserMappings()
        {
            CreateMap<ApplicationUser, UserRegistrationRequestDto>().ReverseMap();
            CreateMap<ApplicationUser, UserLoginRequestDto>().ReverseMap();
            CreateMap<ApplicationUser, GetUserResponseDto>()
                .ForMember(dest => dest.CityName, opt => opt.MapFrom(src => src.City.Name))
                .ForMember(dest => dest.RoleName, opt => opt.MapFrom(src => src.Role.Name))
                .ForMember(dest => dest.FullName, opt => opt.MapFrom(src => src.FirstName + " " + src.LastName))
                .ReverseMap();
            CreateMap<ApplicationUser,UserOverviewResponseDto>()
                .ForMember(dest => dest.FullName, opt => opt.MapFrom(src => src.FirstName + " " + src.LastName))
                .ForMember(dest => dest.CityName, opt => opt.MapFrom(src => src.City.Name))
                .ForMember(dest => dest.RoleName, opt => opt.MapFrom(src => src.Role.Name))
                .ReverseMap();

            
        }
    }
}
