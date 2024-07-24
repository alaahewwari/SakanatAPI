using AutoMapper;
using BusinessLogic.Contracts.General;
using BusinessLogic.DTOs.NotificationDtos.Responses;
using BusinessLogic.ErrorHandling;
using BusinessLogic.Repositories.Interfaces;
using BusinessLogic.Services.IdentityServices.Interfaces;
using BusinessLogic.Services.NotificationServices.Interfaces;
using DataAccess.Enums.Notification;
using DataAccess.Models;
using ErrorOr;
using Sieve.Models;

namespace BusinessLogic.Services.NotificationServices.Implementations
{
    public class NotificationServices(
        IIdentityManager identityManager,
        INotificationRepository notificationRepository,
        IMapper mapper
        ) : INotificationServices
    {
        public async Task CreateNotificationAsync(NotificationType type,Guid senderId, List<Guid>? receiverIds , Guid? apartmentId)
        {
            var sender = await identityManager.FindByIdAsync(senderId);
            DateTimeOffset utcDate = DateTimeOffset.UtcNow; // Example UTC date
            TimeZoneInfo timeZone = TimeZoneInfo.FindSystemTimeZoneById("Israel Standard Time"); // Specify the time zone
            DateTimeOffset localDateTimeOffset = TimeZoneInfo.ConvertTime(utcDate, timeZone); // Convert to the specified time zone

            DateTime localDateTime = localDateTimeOffset.DateTime; // Convert to DateTime

            var notification = new Notification
            {
                ApartmentId = apartmentId,
                SenderId = senderId,
                CreationDate = localDateTime,
                Status = NotificationStatus.Unread,
                Type = type,
            };
            foreach (var receiverId in receiverIds)
            {
                var receiver = await identityManager.FindByIdAsync(receiverId);
                await notificationRepository.CreateNotificationAsync(notification, sender, receiver);
            }
        }
        public async Task<ErrorOr<IList<NotificationResponseDto>>> GetAllNotificationsByUserIdAsync(SieveModel sieveModel, Guid userId)
        {
            var user = await identityManager.FindByIdAsync(userId);
            if (user is null)
            {
                return Errors.Identity.UserNotFound;
            }
            var result =await notificationRepository.GetAllNotificationsByUserIdAsync(sieveModel, userId);

            var response = mapper.Map<IList<NotificationResponseDto>>(result);
            return response.ToList();

        }
        public async Task<ErrorOr<SuccessResponse>> UpdateNotificationsStatusAsync(Guid notificationId)
        {
            var user = await identityManager.GetLoggedInUserIdAsync();
            if (user is null)
            {
                return Errors.Identity.Unauthorized;
            }
            var userId = user.Id;
            var notification = await notificationRepository.GetNotificationByIdAsync(notificationId);
            if (notification is null)
            {
                return Errors.Notification.NotificationNotFound;
            }
            var result = await notificationRepository.UpdateNotificationsStatusAsync(userId, notificationId);
            if (result is null)
            {
                return Errors.Unknown.Create("Failed to update notification status");
            }
            return new SuccessResponse($"Notification status updated successfully to {result.Status.ToString()}");
        }
        public async Task<ErrorOr<NotificationResponseDto>> GetNotificationByIdAsync(Guid notificationId)
        {
            var notification = await notificationRepository.GetNotificationByIdAsync(notificationId);
            if (notification is null)
            {
                return Errors.Notification.NotificationNotFound;
            }
            
            var response = mapper.Map<NotificationResponseDto>(notification);
            return response;
        }

    }
}
