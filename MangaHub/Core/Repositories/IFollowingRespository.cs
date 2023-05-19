using MangaHub.Core.Models;
using MangaHub.Core.Repositories;

namespace MangaHub.Core
{
    public interface IFollowingRespository : IRepository<Following>
    {
        Following GetFollowing(string followerId, string followeeId);
        Following GetFollowingWithUsers(string followerId, string followeeId);
    }
}