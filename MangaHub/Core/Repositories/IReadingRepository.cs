using MangaHub.Core.Models;
using System.Collections.Generic;

namespace MangaHub.Core.Repositories
{
    public interface IReadingRepository : IRepository<Reading>
    {
        Reading GetReadingForManga(int mangaId, string userId);
        IEnumerable<Reading> GetReadingsForUser(string userId);
    }
}
