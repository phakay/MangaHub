using MangaHub.Core.Models;

namespace MangaHub.Core.Repositories
{
    public interface IChapterRepository : IRepository<Chapter>
    {
        Chapter GetChapterForManga(int mangaId, int chapterNo);
    }
}
