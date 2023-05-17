using MangaHub.Core.Models;
using System.Collections.Generic;

namespace MangaHub.Core.Repositories
{
    public interface INotificationRepository : IRepository<Notification>
    {
        IEnumerable<Notification> GetNotificationsForUser(string userId);
        
    }
}
