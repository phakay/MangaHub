using MangaHub.Core.Models;
using System.Data.Entity.ModelConfiguration;

namespace MangaHub.Persistence.EntityConfigurations
{
    public class UserNotificationConfiguration : EntityTypeConfiguration<UserNotification>
    {
        public UserNotificationConfiguration()
        {
            Property(un => un.NotificationId).HasColumnOrder(0);
            Property(un => un.UserId).HasColumnOrder(1);
            HasKey(un => new { un.NotificationId, un.UserId });
        }
    }
}