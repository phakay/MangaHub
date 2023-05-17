using MangaHub.Core.Models;
using System.Data.Entity.ModelConfiguration;

namespace MangaHub.Persistence.EntityConfigurations
{
    public class UserNotificationConfiguration : EntityTypeConfiguration<UserNotification>
    {
        public UserNotificationConfiguration()
        {
            HasKey(un => new { un.NotificationId, un.UserId });

            HasRequired(un => un.User)
                .WithMany(u => u.UserNotifications)
                .WillCascadeOnDelete(true);
        }
    }
}