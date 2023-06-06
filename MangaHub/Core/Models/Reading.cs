namespace MangaHub.Core.Models
{
    public class Reading
    {
        public int MangaId { get; set; }
        public Manga Manga { get; set; }
        public string UserId { get; set; }
        public ApplicationUser User { get; set; }

        public Reading()
        {}

        public Reading(ApplicationUser user, Manga manga)
        {
            User = user ?? throw new System.ArgumentNullException(nameof(user));
            Manga = manga ?? throw new System.ArgumentNullException(nameof(manga));
        }
    }
}