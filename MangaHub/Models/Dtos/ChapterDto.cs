namespace MangaHub.Models.Dtos
{
    public class ChapterDto
    {
        public int MangaId { get; set; }
        public int ChapterNo { get; set; }
        public int NumberOfPages { get; set; }
        public string Information { get; set; }
    }
}