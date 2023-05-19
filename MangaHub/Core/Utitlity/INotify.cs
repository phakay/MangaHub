using MangaHub.Core.Enums;
using MangaHub.Core.Models;
using System.Collections.Generic;

namespace MangaHub.Core.Utitlity
{
    public interface INotify
    {
        void NotifyCreate();
        void NotifyDelete();
        void AddNotification(NotificationType notificationType, string message, IEnumerable<ApplicationUser> usersToNotify);
    }
}
