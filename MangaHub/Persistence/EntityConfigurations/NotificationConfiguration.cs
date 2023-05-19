using MangaHub.Core.Models;
using System.Data.Entity.ModelConfiguration;

namespace MangaHub.Persistence.EntityConfigurations
{
    public class NotificationConfiguration : EntityTypeConfiguration<Notification>
    {
        public NotificationConfiguration()
        {
            Property(n => n.Message).IsRequired();
        }
    }
}