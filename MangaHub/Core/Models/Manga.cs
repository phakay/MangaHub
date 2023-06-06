using MangaHub.Utility;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;

namespace MangaHub.Core.Models
{
    public class Manga : INotifiableOnUpdate<Manga>
    {
        public int Id { get; set; }
        public ApplicationUser Artist { get; set; }
        public string ArtistId { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public Genre Genre { get; set; }
        public byte GenreId { get; set; }
        public DateTime DateCreated { get; private set; }
        public byte[] Picture { get; set; }
        public ICollection<Chapter> Chapters { get; private set; }
        public ICollection<Reading> Readings { get; private set; }
        public Manga()
        {
            DateCreated = DateTime.Now;
            Chapters = new Collection<Chapter>();
            Readings = new Collection<Reading>();
        }

        public void Update(Manga objWithUpdate) 
        {
            Title = objWithUpdate.Title;
            Description = objWithUpdate.Description;
            GenreId = objWithUpdate.GenreId;
            Picture = objWithUpdate.Picture;
        }

        public string GetNotificationMessageForUpdate(Manga objWithUpdate)
        {
            var changes = new Collection<string>();
            var originalValues = new Collection<string>();
            var newValues = new Collection<string>();
            var message = string.Empty;

            if (Title != objWithUpdate.Title)
            {
                changes.Add("Title");
                originalValues.Add(Title);
                newValues.Add(objWithUpdate.Title);
            }
            if (Description != objWithUpdate.Description)
            {
                changes.Add("Description");
            }
            if (GenreId != objWithUpdate.GenreId)
            {
                changes.Add("Genre");
                originalValues.Add(Genre.Name);
                newValues.Add(objWithUpdate.Genre.Name);
            }
            if (objWithUpdate.Picture != null && !Enumerable.SequenceEqual(Picture, objWithUpdate.Picture))
            {
                changes.Add("Picture");
            }

            if (changes.Count > 0)
            {
                var changesString = changes.Count > 1 ? string.Join(" and ", changes) : changes[0];
                message = $"{Artist.Name} has updated {changesString} of {Title}" +
                $" from {string.Join(" / ", originalValues)} to {string.Join(" / ", newValues)}";
            }

            return message;
        }
    }
}