using MangaHub.Core.Models;
using MangaHub.Core.Repositories;
using System.Collections.Generic;
using System.Linq;

namespace MangaHub.Persistence.Repositories
{
    public class UserNotificationRepo : IUserNotificationRepository
    {
        private readonly ApplicationDbContext _context;

        public UserNotificationRepo(ApplicationDbContext context)
        {
            _context = context;
        }

        public IEnumerable<UserNotification> GetUserNotifications(string userId)
        {
            return _context.UserNotifications
                .Where(un => un.UserId == userId && !un.IsRead)
                .ToList();
        }
    }
}