
using AutoMapper;
using BusinessLogic.DTOs.ContractDtos.Requests;
using BusinessLogic.DTOs.ContractDtos.Responses;
using DataAccess.Enums.Apartment;
using DataAccess.Models;

namespace BusinessLogic.MappingProfiles
{
    public class ContractMappings : Profile
    {
        public ContractMappings()
        {
                 CreateMap<CreateContractRequestDto, Contract>()
                .ForMember(dest => dest.type, opt => opt.MapFrom(src => (RentPeriod)src.Type));
                 CreateMap<Contract, ContractResponseDto>()
                .ForMember(dest => dest.TenantName, opt => opt.MapFrom(src => src.Tenant.FirstName + " " + src.Tenant.LastName))
                .ForMember(dest => dest.Currency, opt => opt.MapFrom(src => (PriceCurrency)src.Currency))
                .ForMember(dest => dest.Type, opt => opt.MapFrom(src => (RentPeriod)src.type))
                .ForMember(dest => dest.TotalPayments, opt => opt.MapFrom(src => src.PaymentLogs.Sum(p => p.Amount)))
                .ForMember(dest => dest.RemainingPayments, opt => opt.MapFrom(src => src.RentPrice - src.PaymentLogs.Sum(p => p.Amount)))
                .ForMember(dest => dest.IsTerminated, opt => opt.MapFrom(src =>
                ((src.PaymentLogs.Sum(a=> a.Amount)) >= src.RentPrice)));
        }
    }
}
