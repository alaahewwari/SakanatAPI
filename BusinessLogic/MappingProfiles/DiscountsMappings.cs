using BusinessLogic.DTOs.ApartmentDtos.Responses;
using BusinessLogic.DTOs.DiscountDtos.Requests;
using BusinessLogic.DTOs.DiscountDtos.Responses;
using AutoMapper;
using DataAccess.Models;

namespace BusinessLogic.MappingProfiles
{
    public class DiscountsMappings : Profile
    {
        public DiscountsMappings()
        {
            CreateMap<Discount, DiscountResponseDto>()
                .ForMember(dest => dest.IsAdded, opt
                => opt.MapFrom(src => src.ApartmentDiscounts.Any()))
                .ForMember(dest => dest.ApartmentsCount,
                       opt
                       => opt.MapFrom(src => src.ApartmentDiscounts.Count));

            CreateMap<Discount, GetApartmentDiscountsResponseDto>()
                .ForMember(dest => dest.ExpiryDate, opt
                               => opt.MapFrom(src => src.ApartmentDiscounts.FirstOrDefault()!.ExpiresAt));
            CreateMap<Discount, DiscountRequestDto>().ReverseMap();

            CreateMap<Apartment, ApartmentDiscountsResponseDto>()
                .ForMember(dest => dest.ApartmentName, opt => opt.MapFrom(src => src.Name))
                .ForMember(dest => dest.DiscountsCount, opt => opt.MapFrom(src => src.ApartmentDiscounts.Count));
        }
    }
}
