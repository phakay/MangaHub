using System;

namespace MangaHub.Core.Models
{
    public class Chapter
    {
        public Manga Manga { get; set; }
        public int MangaId { get; set; }
        public int ChapterNo { get; set; }
        public int NumberOfPages { get; set; }
        public string Information { get; set; }
        public DateTime DateTime { get; set; }
    }
}