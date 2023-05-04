using MangaHub.Core.Models;
using System.Collections.Generic;

namespace MangaHub.Core.Repositories
{
    public interface IMangaRepository
    {
        Manga GetManga(int id);
        IEnumerable<Manga> GetMangaWithChapters();
    }
}