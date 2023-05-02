using System;

namespace MangaHub.Models
{
    public class Manga
    {
        public int Id { get; set; }
        public ApplicationUser Artist { get; set; }
        public string ArtistId { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public Genre Genre { get; set; }
        public byte GenreId { get; set; }
        public DateTime DateCreated { get; set; }
        public byte[] Picture { get; set; }
    }
}