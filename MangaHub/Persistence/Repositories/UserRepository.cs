using MangaHub.Core.Models;
using MangaHub.Core.Repositories;
using System.Collections.Generic;
using System.Linq;

namespace MangaHub.Persistence.Repositories
{
    public class UserRepository : Repository<ApplicationUser>, IApplicationUserRepository
    {
        private readonly IApplicationDbContext _context;

        public UserRepository(IApplicationDbContext context)
            : base(context.Users)
        {
            _context = context;
        }

        public ApplicationUser GetUser(string Id)
        {
            return _context.Users.Find(Id);
        }

        public IEnumerable<ApplicationUser> GetFollowees(string followerId)
        {
            return _context.Followings
                .Where(f => f.FollowerId == followerId)
                .Select(f => f.Followee).ToList();   
        }
    }
}