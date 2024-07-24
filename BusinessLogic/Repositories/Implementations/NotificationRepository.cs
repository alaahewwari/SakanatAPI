using BusinessLogic.Repositories.Interfaces;
using DataAccess;
using DataAccess.Models;
using Microsoft.AspNetCore.SignalR;
using Sieve.Models;
using Microsoft.EntityFrameworkCore;
using Sieve.Services;
using DataAccess.Enums.Notification;
using BusinessLogic.Infrastructure.SignalR;
using MimeKit;
namespace BusinessLogic.Repositories.Implementations
{
    public class NotificationRepository(
        IHubContext<SignalServer> hubContext,
        ApplicationDbContext context,
        ISieveProcessor sieveProcessor
        ) : INotificationRepository
    {
        public async Task<bool> CreateNotificationAsync(Notification notification,ApplicationUser? sender, ApplicationUser? receiver)
        {
            receiver?.Notifications.Add(notification);
            await context.SaveChangesAsync();
            return true;
        }

        public async Task<IList<Notification>> GetAllNotificationsByUserIdAsync(SieveModel sieveModel, Guid userId)
        {
            var notifications = context.Users.Where(u => u.Id == userId)
                .SelectMany(u => u.Notifications)
                .AsNoTracking();
                var result = await sieveProcessor.Apply(sieveModel, notifications).ToListAsync();
            return result;
        }

        public async Task<Notification?> GetNotificationByIdAsync(Guid id)
        {
            var notification = await context.Notifications
                .AsNoTracking()
                .FirstOrDefaultAsync(n => n.Id == id);
            return notification;
        }
        public async Task<Notification> UpdateNotificationsStatusAsync(Guid userId,Guid notificationId)
        {
            var notification = context.Users
                .Where(u => u.Id == userId)
                .SelectMany(n=> n.Notifications)
                .Where(n => n.Id == notificationId && n.Status == NotificationStatus.Unread)
                .FirstOrDefault();
            if (notification is not null)
            {
                if (notification.Status == NotificationStatus.Read)
                {
                    notification.Status = NotificationStatus.Unread;
                    
                }else
                {
                    notification.Status = NotificationStatus.Read;
                }
                await context.SaveChangesAsync();
                return notification;
            }
            return null;
            
        }
        public async Task<bool> DeleteNotificationAsync(Guid id)
        {
            var notification = await context.Notifications
                .FirstOrDefaultAsync(n => n.Id == id);
            if (notification is not null)
            {
                context.Notifications.Remove(notification);
                await context.SaveChangesAsync();
                return true;
            }
            return false;
        }
        public async Task<Notification?> GetNotificationByTypeAsync(NotificationType type, Guid senderId, Guid receiverId)
        {
            var notification = await context.Notifications
                .AsNoTracking()
                .FirstOrDefaultAsync(n => n.Type == type && n.SenderId == senderId && n.Receivers.Any(r => r.Id == receiverId));
            return notification;
        }   
    }
}
