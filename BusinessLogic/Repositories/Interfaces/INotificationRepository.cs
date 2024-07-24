using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataAccess.Enums.Notification;
using DataAccess.Models;
using Sieve.Models;

namespace BusinessLogic.Repositories.Interfaces
{
    public interface INotificationRepository
    { 
        Task<bool> CreateNotificationAsync(Notification notification, ApplicationUser? sender, ApplicationUser? receiver);
        Task<IList<Notification>> GetAllNotificationsByUserIdAsync(SieveModel sieveModel,Guid userId);
        Task<Notification?> GetNotificationByIdAsync(Guid id);
        Task<Notification> UpdateNotificationsStatusAsync(Guid userId,Guid notificationId);
        Task<bool> DeleteNotificationAsync(Guid id);
        Task<Notification?> GetNotificationByTypeAsync(NotificationType type, Guid senderId, Guid receiverId);
    }
}
