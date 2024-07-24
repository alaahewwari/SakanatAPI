using DataAccess.Enums.Notification;
namespace DataAccess.Models;
public class Notification
{
    public Guid Id { get; set; }
    public DateTime CreationDate { get; set; }
    public NotificationStatus Status { get; set; } = NotificationStatus.Unread;
    public NotificationType Type { get; set; } 
    public Guid? ApartmentId { get; set; }
    public Guid SenderId { get; set; }
    public Apartment? Apartment { get; set; }
    public ApplicationUser Sender { get; set; }
    public ICollection<ApplicationUser>? Receivers { get; set; } = [];
}