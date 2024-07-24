namespace DataAccess.Enums.Notification;
/// <summary>
/// Represents the type of notification.
/// the type of notification can be NewApartment, NewDiscount, NewFollower
/// </summary>
public enum NotificationType : byte
{
    None = 0,
    NewApartment=1,
    NewDiscount = 2,
    NewFollower = 3
}