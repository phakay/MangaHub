using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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
        [Required]
        public string Title { get; set; }
        [Required]
        public string Description { get; set; }
        public Genre Genre { get; set; }
        [Required]
        public byte GenreId { get; set; }
        public DateTime DateCreated { get; set; }
        [Required]
        public byte[] Picture { get; set; }
        public ICollection<Chapter> Chapters { get; private set; }
        public Manga()
        {
            Chapters = new Collection<Chapter>();
        }
    }
}