using MangaHub.Core.Repositories;

namespace MangaHub.Core
{
    public interface IUnitOfWork
    {
        IChapterRepository ChapterRepo { get; set; }
        IGenreRepository GenreRepo { get; set; }
        IMangaRepository MangaRepo { get; set; }
        IReadingRepository ReadingRepo { get; set; }

        void Complete();
    }
}