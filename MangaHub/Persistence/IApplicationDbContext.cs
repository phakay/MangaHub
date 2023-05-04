using MangaHub.Core.Models;
using System.Data.Entity;

namespace MangaHub.Persistence
{
    public interface IApplicationDbContext
    {
        DbSet<Chapter> Chapters { get; set; }
        DbSet<Genre> Genres { get; set; }
        DbSet<Manga> Mangas { get; set; }
    }
}