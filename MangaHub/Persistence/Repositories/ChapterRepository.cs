using MangaHub.Core.Models;
using MangaHub.Core.Repositories;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;

namespace MangaHub.Persistence.Repositories
{
    public class ChapterRepository : Repository<Chapter>, IChapterRepository
    {
        private readonly IApplicationDbContext _context;

        public ChapterRepository(IApplicationDbContext context)
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

        public Chapter GetChapterForMangaWithReaders(int mangaId, int chapterNo)
        {
            return _context.Chapters.Include(c => c.Manga.Readings.Select(r => r.User))
                                    .SingleOrDefault(c => c.MangaId == mangaId &&
                                    c.ChapterNo == chapterNo);

        }

        public IEnumerable<Chapter> GetChaptersForManga(int mangaId)
        {
            return _context.Chapters
                .Where(c => c.MangaId == mangaId).ToList();
        }
    }
}