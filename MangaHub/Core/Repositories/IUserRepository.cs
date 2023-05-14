using MangaHub.Core.Models;

namespace MangaHub.Core.Repositories
{
    public interface IUserRepository : IRepository<ApplicationUser>
    {
        ApplicationUser GetUser(string followeeId);
    }
}