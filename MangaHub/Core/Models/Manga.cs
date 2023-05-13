using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace MangaHub.Core.Models
{
    public class Manga
    {
        public int Id { get; set; }
        public ApplicationUser Artist { get; set; }
        public string ArtistId { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public Genre Genre { get; set; }
        public byte GenreId { get; set; }
        public DateTime DateCreated { get; set; }
        public byte[] Picture { get; set; }
        public ICollection<Chapter> Chapters { get; private set; }
        public ICollection<Reading> Readings { get; private set; }
        public Manga()
        {
            Chapters = new Collection<Chapter>();
            Readings = new Collection<Reading>();
        }

        public void Update(Manga objToUpdate)
        {
            Title = objToUpdate.Title;
            Description = objToUpdate.Description;
            GenreId = objToUpdate.GenreId;
            Picture = objToUpdate.Picture;
        }
    }
}