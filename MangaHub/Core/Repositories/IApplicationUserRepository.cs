using MangaHub.Core.Models;
using System.Collections.Generic;

namespace MangaHub.Core.Repositories
{
    public interface IApplicationUserRepository : IRepository<ApplicationUser>
    {
        ApplicationUser GetUser(string Id);
        IEnumerable<ApplicationUser> GetFollowees(string followerId);
    }
}