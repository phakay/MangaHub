using MangaHub.Core.Models;

namespace MangaHub.Core.ViewModels
{
    public class MangaDetailsViewModel
    {
        public Manga Manga { get; set; }
        public bool IsReading { get; internal set; }
        public bool IsFollowing { get; internal set; }
    }
}