
using ErrorOr;
using System;

namespace BusinessLogic.ErrorHandling
{
    public static partial class Errors
    {
        public static class Notification
        {
            public static Error NotificationNotFound => Error.NotFound(
                               "Notification.NotificationNotFound",
                               "Notification not found!"
                           );

            public static Error NoNotificationsFound => Error.NotFound(
                               "Notification.NoNotificationsFound",
                               "No notifications found!"
                           );
        }
    }
}
