using MangaHub.Core.Models;
using System.Collections.Generic;

namespace MangaHub.Core.Repositories
{
    public interface IMangaRepository : IRepository<Manga>
    {
        Manga GetManga(int id);
        IEnumerable<Manga> GetMangaWithChapters(string userId = null);
    }
}