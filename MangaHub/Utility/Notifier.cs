using MangaHub.Core.Enums;
using MangaHub.Core.Models;
using System.Collections.Generic;

namespace MangaHub.Utility
{
    public class Notifier
    {
        public void NotifyCreate(IEnumerable<ApplicationUser> usersToNotify, string notificationMessage)
        {
            AddNotification(NotificationType.Created, notificationMessage, usersToNotify);
        }
        public void NotifyDelete(IEnumerable<ApplicationUser> usersToNotify, string notificationMessage)
        {
            AddNotification(NotificationType.Deleted, notificationMessage, usersToNotify);
        }
        public void NotifyUpdate(IEnumerable<ApplicationUser> usersToNotify, string notificationMessage)
        {
            AddNotification(NotificationType.Updated, notificationMessage, usersToNotify);
        }
        public void AddNotification(NotificationType notificationType, string message, IEnumerable<ApplicationUser> users)
        {
            var notification = Notification.Add(notificationType, message);
            foreach (var user in users)
            {
                user.Notify(notification);
            }
        }

    }
}