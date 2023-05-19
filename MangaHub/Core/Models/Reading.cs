using MangaHub.Core.Enums;
using MangaHub.Core.Utitlity;
using System.Collections.Generic;

namespace MangaHub.Core.Models
{
    public class Reading : INotify
    {
        public int MangaId { get; set; }
        public Manga Manga { get; set; }
        public string UserId { get; set; }
        public ApplicationUser User { get; set; }

        public Reading()
        {}

        public Reading(ApplicationUser user, Manga manga)
        {
            User = user ?? throw new System.ArgumentNullException(nameof(user));
            Manga = manga ?? throw new System.ArgumentNullException(nameof(manga));
        }

        public void NotifyCreate()
        {  
            AddNotification(NotificationType.Created, $"{User.Name} is reading Manga: {Manga.Title}", new[] { Manga.Artist});
        }

        public void NotifyDelete()
        {
            AddNotification(NotificationType.Deleted, $"{User.Name} has unread Manga: {Manga.Title}", new[] { Manga.Artist});
        }

        public void AddNotification(NotificationType notificationType, string message, IEnumerable<ApplicationUser> usersToNotify)
        {
            var notification = Notification.Add(notificationType, message);
            foreach (var user in usersToNotify)
            {
                user.Notify(notification);
            }
        }
    }
}