
using AutoMapper;
using BusinessLogic.DTOs.PaymentDtos.Responses;
using DataAccess.Models;

namespace BusinessLogic.MappingProfiles
{
    public class PaymentLogMappings: Profile
    {
public PaymentLogMappings()
        {
            CreateMap<PaymentLog, PaymentResponseDto>().ReverseMap();
            CreateMap<PaymentResponseDto, PaymentLog>().ReverseMap();
        }
    }
}
