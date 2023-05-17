using MangaHub.Core.Models;
using System.Collections.Generic;

namespace MangaHub.Core.Repositories
{
    public interface IChapterRepository : IRepository<Chapter>
    {
        Chapter GetChapterForManga(int mangaId, int chapterNo);
        Chapter GetChapterForMangaWithReaders(int mangaId, int chapterNo);
        IEnumerable<Chapter> GetChaptersForManga(int mangaId);
    }
}
