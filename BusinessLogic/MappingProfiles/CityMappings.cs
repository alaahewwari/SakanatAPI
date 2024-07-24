using AutoMapper;
using BusinessLogic.DTOs.CityDtos.Requests;
using BusinessLogic.DTOs.CityDtos.Responses;
using DataAccess.Models;


namespace BusinessLogic.MappingProfiles
{
    public class CityMappings : Profile
    {
        public CityMappings()
        {
            CreateMap<City, CityRequestDto>().ReverseMap();
            CreateMap<City, CityResponseDto>()
                .ForMember(dest => dest.NumberOfApartments, opt => opt.MapFrom(src => src.Apartments.Count()))
                .ReverseMap();
        }
    }
}
