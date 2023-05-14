using MangaHub.Core;
using MangaHub.Core.Models;

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
    }
}