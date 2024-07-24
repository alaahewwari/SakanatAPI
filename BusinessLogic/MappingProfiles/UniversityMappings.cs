using AutoMapper;
using BusinessLogic.Dtos.University;
using BusinessLogic.DTOs.UniversityDtos.Requests;
using DataAccess.Models;
namespace BusinessLogic.MappingProfiles
{
    public class UniversityMappings : Profile
    {
        public UniversityMappings()
        {
            CreateMap<University, UniversityRequestDto>().ReverseMap();
            CreateMap<University, UniversityResponseDto>()
                .ForMember(dest=>dest.NumberOfApartments,
                opt=>opt.MapFrom(src=>src.Apartments.Count()))
                .ReverseMap();
        }
    }
}
