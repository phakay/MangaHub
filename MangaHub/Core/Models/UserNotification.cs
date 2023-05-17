using System;

namespace MangaHub.Core.Models
{
    public class UserNotification
    {
        public string UserId { get; set; }
        public ApplicationUser User { get; set; }
        public int NotificationId { get; set; }
        public Notification Notification { get; set; }
        public bool IsRead { get; private set; }

        protected UserNotification() { }

        public UserNotification(ApplicationUser user, Notification notification)
        {
            User = user ?? throw new ArgumentNullException(nameof(user));
            Notification = notification ?? throw new ArgumentNullException(nameof(notification));
        }
        public UserNotification(string userId, Notification notification)
        {
            UserId = userId ?? throw new ArgumentNullException(nameof(userId));
            Notification = notification ?? throw new System.ArgumentNullException(nameof(notification));
        }
        public void Read()
        {
            IsRead = true;
        }
    }
}