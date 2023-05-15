using MangaHub.Core.Models;
using System.Data.Entity;

namespace MangaHub.Persistence
{
    public interface IApplicationDbContext
    {
        DbSet<Chapter> Chapters { get; set; }
        DbSet<Genre> Genres { get; set; }
        DbSet<Manga> Mangas { get; set; }
        DbSet<Reading> Readings { get; set; }
        DbSet<Following> Followings { get; set; }
        DbSet<Notification> Notifications { get; set; }
        DbSet<UserNotification> UserNotifications { get; set; }
        IDbSet<ApplicationUser> Users { get; set; }
    }
}