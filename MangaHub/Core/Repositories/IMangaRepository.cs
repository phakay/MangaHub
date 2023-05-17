using MangaHub.Core.Models;
using System.Collections.Generic;

namespace MangaHub.Core.Repositories
{
    public interface IMangaRepository : IRepository<Manga>
    {
        Manga GetManga(int id);
        Manga GetMangaWithReaders(int id);
        IEnumerable<Manga> GetMangasReadByUser(string userId);
        IEnumerable<Manga> GetMangasWithChapters(string userId = null);
    }
}