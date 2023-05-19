using MangaHub.Core.Models;
using System.Collections.Generic;

namespace MangaHub.Core.Repositories
{
    public interface IGenreRepository : IRepository<Genre>
    {
        Genre Get(int id);
        List<Genre> GetGenres();
    }
}