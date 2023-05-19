using MangaHub.Core.Models;
using MangaHub.Core.Repositories;
using System.Collections.Generic;
using System.Linq;

namespace MangaHub.Persistence.Repositories
{
    public class GenreRepository : Repository<Genre>, IGenreRepository
    {
        private readonly IApplicationDbContext _context;

        public GenreRepository(IApplicationDbContext context) : base(context.Genres)
        {
            _context = context;
        }

        public Genre Get(int id)
        {
            return _context.Genres.Find(id);
        }

        public List<Genre> GetGenres()
        {
            return _context.Genres.ToList();
        }

    }
}