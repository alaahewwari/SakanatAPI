namespace DataAccess.Enums.Notification;
/// <summary>
/// Represents the status of a notification
/// that can be either Unread or Read. 
/// </summary>
public enum NotificationStatus : byte
{
    None=0,
    Unread= 1,
    Read = 2
}