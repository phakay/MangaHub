using MangaHub.Core.Models;
using MangaHub.Core.Repositories;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;

namespace MangaHub.Persistence.Repositories
{
    public class MangaRepository : Repository<Manga>, IMangaRepository
    {
        private readonly IApplicationDbContext _context;

        public MangaRepository(IApplicationDbContext context)
            : base(context.Mangas)
        {
            _context = context;
        }

        public Manga GetManga(int id)
        {
            return _context.Mangas
                            .Include(g => g.Genre)
                            .SingleOrDefault(m => m.Id == id);
        }

        public IEnumerable<Manga> GetMangaWithChapters(string userId = null)
        {
            var query = _context.Mangas
                            .Include(m => m.Artist)
                            .Include(m => m.Chapters);

            if (userId != null)
                query.Where(m => m.ArtistId == userId);

            return query.ToList();
        }
    }
}