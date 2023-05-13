using MangaHub.Core;
using MangaHub.Core.Repositories;
using MangaHub.Persistence.Repositories;

namespace MangaHub.Persistence
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly ApplicationDbContext _context;

        public IMangaRepository MangaRepo { get; set; }
        public IChapterRepository ChapterRepo { get; set; }
        public IGenreRepository GenreRepo { get; set; }
        public IReadingRepository ReadingRepo { get ; set ; }

        public UnitOfWork(ApplicationDbContext context)
        {
            _context = context;
            MangaRepo = new MangaRepository(_context);
            ChapterRepo = new ChapterRepository(_context);
            GenreRepo = new GenreRepository(_context);
            ReadingRepo = new ReadingRepository(_context);
        }

        public void Complete()
        {
            _context.SaveChanges();
        }
    }
}