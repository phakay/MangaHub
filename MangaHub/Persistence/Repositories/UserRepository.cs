using MangaHub.Core.Models;
using MangaHub.Core.Repositories;

namespace MangaHub.Persistence.Repositories
{
    public class UserRepository : Repository<ApplicationUser>, IUserRepository
    {
        private readonly IApplicationDbContext _context;

        public UserRepository(IApplicationDbContext context)
            : base(context.Users)
        {
            _context = context;
        }

        public ApplicationUser GetUser(string followeeId)
        {
            return _context.Users.Find(followeeId);
        }
    }
}