using AutoMapper;
using BusinessLogic.DTOs.ApartmentDtos.Requests;
using BusinessLogic.DTOs.ApartmentDtos.Responses;
using BusinessLogic.DTOs.EnumDtos;
using DataAccess.Enums.Apartment;
using DataAccess.Models;


namespace BusinessLogic.MappingProfiles
{
    public class ApartmentMappings : Profile
    {
        public ApartmentMappings()
        {
            CreateMap<ApartmentRequestDto, Apartment>()
                .ForMember(dest => dest.FurnishedStatus, opt => opt.MapFrom(src => (FurnishedStatus)src.FurnishedStatus))
                .ForMember(dest => dest.PriceCurrency, opt => opt.MapFrom(src => (PriceCurrency)src.PriceCurrency))
                .ForMember(dest => dest.GenderAllowed, opt => opt.MapFrom(src => (GenderAllowed)src.GenderAllowed))
                .ReverseMap();

            CreateMap<Apartment, ApartmentResponseDto>()
                .ForMember(dest => dest.CityName, opt => opt.MapFrom(src => src.City.Name))
                .ForMember(dest => dest.UniversityName, opt => opt.MapFrom(src => src.NearbyUniversity.Name))
                .ForMember(dest => dest.IsDiscounted, opt => opt.MapFrom(src => src.ApartmentDiscounts.Any() == true))
                .ReverseMap();

            CreateMap<Apartment, ApartmentOverviewResponseDto>()
                .ForMember(dest => dest.CityName, opt => opt.MapFrom(src => src.City.Name))
                .ForMember(dest => dest.UniversityName, opt => opt.MapFrom(src => src.NearbyUniversity.Name))
                .ForMember(dest => dest.PriceCurrency, opt => opt.MapFrom(src => src.PriceCurrency.ToString()))
                .ForMember(dest => dest.IsDiscounted, opt => opt.MapFrom(src => src.ApartmentDiscounts.Any() == true));

            CreateMap<ApartmentToUpdateRequestDto, Apartment>()
                .ForMember(dest => dest.Id, opt => opt.Ignore())
                .ForMember(dest => dest.FurnishedStatus, opt => opt.MapFrom(src => (FurnishedStatus)src.FurnishedStatus))
                .ForMember(dest => dest.PriceCurrency, opt => opt.MapFrom(src => (PriceCurrency)src.PriceCurrency))
                .ForMember(dest => dest.GenderAllowed, opt => opt.MapFrom(src => (GenderAllowed)src.GenderAllowed));

            CreateMap<ApartmentImage, ApartmentImageResponseDto>().ReverseMap();
            CreateMap<Apartment, ApartmentDiscountsResponseDto>()
           .ForMember(dest => dest.ApartmentName, opt => opt.MapFrom(src => src.Name))
           .ForMember(dest => dest.Discounts, opt => opt.MapFrom(src => src.ApartmentDiscounts))
           .ForMember(dest => dest.DiscountsCount, opt => opt.MapFrom(src => src.ApartmentDiscounts.Count));

            CreateMap<FurnishedStatus, FurnishedStatusDto>()
            .ForMember(dest => dest.Name, opt => opt.MapFrom(src => src.ToString()))
            .ForMember(dest => dest.Value, opt => opt.MapFrom(src => (byte)src));

            CreateMap<GenderAllowed, GenderAllowedDto>()
                .ForMember(dest => dest.Name, opt => opt.MapFrom(src => src.ToString()))
                .ForMember(dest => dest.Value, opt => opt.MapFrom(src => (byte)src));

            CreateMap<RentPeriod, RentPeriodDto>()
                .ForMember(dest => dest.Name, opt => opt.MapFrom(src => src.ToString()))
                .ForMember(dest => dest.Value, opt => opt.MapFrom(src => (byte)src));

            CreateMap<PriceCurrency, PriceCurrencyDto>()
                .ForMember(dest => dest.Name, opt => opt.MapFrom(src => src.ToString()))
                .ForMember(dest => dest.Value, opt => opt.MapFrom(src => (byte)src));


        }
    }
}
