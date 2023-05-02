using System;
using System.ComponentModel.DataAnnotations;

namespace MangaHub.Models
{
    public class Manga
    {
        public int Id { get; set; }
        public ApplicationUser Artist { get; set; }
        [Required]
        public string ArtistId { get; set; }
        [MaxLength(255)]
        public string Name { get; set; }
        public string Description { get; set; }
        public Genre Genre { get; set; }
        [Required]
        public byte GenreId { get; set; }
        public DateTime DateCreated { get; set; }
        public byte[] Picture { get; set; }
    }
}