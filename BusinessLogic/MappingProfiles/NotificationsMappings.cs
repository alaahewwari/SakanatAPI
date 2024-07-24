
using AutoMapper;
using BusinessLogic.DTOs.EnumDtos;
using BusinessLogic.DTOs.NotificationDtos.Responses;
using DataAccess.Enums.Notification;
using DataAccess.Models;

namespace BusinessLogic.MappingProfiles
{
    internal class NotificationsMappings : Profile
    {
        public NotificationsMappings() {
            CreateMap<NotificationStatus, NotificationStatusDto>()
                .ForMember(dest => dest.Name, opt => opt.MapFrom(src => src.ToString()))
                .ForMember(dest => dest.Value, opt => opt.MapFrom(src => (byte)src));
            CreateMap<NotificationType, NotificationTypeDto>()
                .ForMember(dest => dest.Name, opt => opt.MapFrom(src => src.ToString()))
                .ForMember(dest => dest.Value, opt => opt.MapFrom(src => (byte)src));
CreateMap<Notification,NotificationResponseDto>().ReverseMap();
        }
    }
}
