using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BusinessLogic.Contracts.General;
using BusinessLogic.DTOs.NotificationDtos.Responses;
using DataAccess.Enums.Notification;
using DataAccess.Models;
using ErrorOr;
using Sieve.Models;

namespace BusinessLogic.Services.NotificationServices.Interfaces
{
    public interface INotificationServices
    {
        public Task CreateNotificationAsync(NotificationType type, Guid senderId, List<Guid> receiverIds,Guid? apartmentId);
        public Task<ErrorOr<IList<NotificationResponseDto>>> GetAllNotificationsByUserIdAsync(SieveModel sieveModel, Guid userId);
        public Task<ErrorOr<SuccessResponse>> UpdateNotificationsStatusAsync(Guid notificationId);
        public Task<ErrorOr<NotificationResponseDto>> GetNotificationByIdAsync(Guid notificationId);
    }
}
