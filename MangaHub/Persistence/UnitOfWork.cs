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
        public IApplicationUserRepository UserRepo { get; set; }
        public IFollowingRespository FollowingRepo { get; set; }
        public INotificationRepository NotificationRepo { get; set; }
        public IUserNotificationRepository UserNotificationRepo { get; set; }

        public UnitOfWork(ApplicationDbContext context)
        {
            _context = context;
            MangaRepo = new MangaRepository(_context);
            ChapterRepo = new ChapterRepository(_context);
            GenreRepo = new GenreRepository(_context);
            ReadingRepo = new ReadingRepository(_context);
            UserRepo = new UserRepository(_context);
            FollowingRepo = new FollowingRepository(_context);
            NotificationRepo = new NotificationRepository(_context);
            UserNotificationRepo = new UserNotificationRepo(_context);
        }

        public void Complete()
        {
            _context.SaveChanges();
        }
    }
}