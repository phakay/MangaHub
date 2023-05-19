using MangaHub.Core;
using MangaHub.Core.Models;
using System.Data.Entity;
using System.Linq;

namespace MangaHub.Persistence.Repositories
{
    public class FollowingRepository : Repository<Following>, IFollowingRespository
    {
        private readonly IApplicationDbContext _context;

        public FollowingRepository(IApplicationDbContext context)
            : base(context.Followings)
        {
            _context = context;
        }

        public Following GetFollowing(string followerId, string followeeId)
        {
            // DbSet.Find will take ordering into consideration
            // when using a composite primary key.
            // according to ordering, composit key is :(followeeId, followerId)
            return _context.Followings.Find(followeeId, followerId);
        }

        public Following GetFollowingWithUsers(string followerId, string followeeId)
        {
            return _context.Followings
                .Include(f => f.Followee)
                .Include(f => f.Follower)
                .SingleOrDefault(f => f.FollowerId == followerId 
                && f.FolloweeId == followeeId);
        }
    }
}