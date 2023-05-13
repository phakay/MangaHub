namespace MangaHub.Core.Models
{
    public class Reading
    {
        public int MangaId { get; set; }
        public Manga Manga { get; set; }
        public string UserId { get; set; }
        public ApplicationUser User { get; set; }
    }
}