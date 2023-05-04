using MangaHub.Core.Models;
using MangaHub.Core.Repositories;
using System.Linq;

namespace MangaHub.Persistence.Repositories
{
    public class ChapterRepository : Repository<Chapter>, IChapterRepository
    {
        private readonly ApplicationDbContext _context;

        public ChapterRepository(ApplicationDbContext context)
            : base(context.Chapters)
        {
            _context = context;
        }

        public Chapter GetChapterForManga(int mangaId, int chapterNo)
        {
            return _context.Chapters
                            .SingleOrDefault(c => c.MangaId == mangaId &&
                            c.ChapterNo == chapterNo);
        }

    }
}