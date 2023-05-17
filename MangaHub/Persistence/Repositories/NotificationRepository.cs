using MangaHub.Core.Models;
using MangaHub.Core.Repositories;
using System.Collections.Generic;
using System.Linq;

namespace MangaHub.Persistence.Repositories
{
    public class NotificationRepository : Repository<Notification>, INotificationRepository
    {
        private readonly IApplicationDbContext _context;

        public NotificationRepository(IApplicationDbContext context)
            : base(context.Notifications)
        {
            _context = context;
        }

        public IEnumerable<Notification> GetNotificationsForUser(string userId)
        {
            return _context.UserNotifications
                .Where(un => un.UserId == userId && !un.IsRead)
                .Select(un => un.Notification)
                .OrderByDescending(un => un.DateCreated);
        }
    }
}