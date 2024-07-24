using BusinessLogic.DTOs.EnumDtos;
using BusinessLogic.DTOs.UserDtos.Responses;
using DataAccess.Enums.Notification;
namespace BusinessLogic.DTOs.NotificationDtos.Responses
{
    public class NotificationResponseDto
    {
        public Guid Id { get; set; }
        public string Description { get; set; }
        public DateTime CreationDate { get; set; }
        public NotificationStatusDto Status { get; set; }
        public NotificationTypeDto Type { get; set; }
        public Guid? ApartmentId { get; set; }
        public Guid SenderId { get; set; }
    }
}
