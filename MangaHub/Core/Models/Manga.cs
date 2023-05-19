using MangaHub.Core.Enums;
using MangaHub.Core.Utitlity;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;

namespace MangaHub.Core.Models
{
    public class Manga : IUpdateableAndNotifiable<Manga>
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

        public void Update(Manga objToUpdate) 
        {
            Title = objToUpdate.Title;
            Description = objToUpdate.Description;
            GenreId = objToUpdate.GenreId;
            Picture = objToUpdate.Picture;
        }

        public void UpdateAndNotify(Manga objToUpdate)
        {
            var changes = new Collection<string>();
            var originalValues = new Collection<string>();
            var newValues = new Collection<string>();
            var message = string.Empty;

            if(Title != objToUpdate.Title)
            {
                changes.Add("Title");
                originalValues.Add(Title);
                newValues.Add(objToUpdate.Title);
            }
            if(Description != objToUpdate.Description)
            {
                changes.Add("Description");
            }
            if (GenreId != objToUpdate.GenreId)
            {
                changes.Add("Genre");
                originalValues.Add(Genre.Name);
                newValues.Add(objToUpdate.Genre.Name);
            }
            if(objToUpdate.Picture != null && !Enumerable.SequenceEqual(Picture, objToUpdate.Picture))
            {
                changes.Add("Picture");
            }

            if(changes.Count > 0)
            {
                var changesString = changes.Count > 1 ? string.Join(" and ", changes) : changes[0];
                message = $"{Artist.Name} has updated {changesString} of {Title}" +
                $" from {string.Join(" / ", originalValues)} to {string.Join(" / ", newValues)}";
            }
            
            Update(objToUpdate);

            if(!string.IsNullOrEmpty(message))
                AddNotification(NotificationType.Created, message, Readings.Select(r => r.User));
        }

        public void NotifyCreate()
        {
            var followers = Artist.Followers.Select(f => f.Follower);
            AddNotification(NotificationType.Created, $"{Artist.Name} uploaded Manga: {Title}", followers);
        }

        public void NotifyDelete()
        {
            AddNotification(NotificationType.Deleted, $"{Artist.Name} removed Manga: {Title}", Readings.Select(r => r.User));
        }

        public void AddNotification(NotificationType notificationType, string message, IEnumerable<ApplicationUser> users)
        {
            var notification = Notification.Add(notificationType, message);
            foreach (var user in users)
            {
                user.Notify(notification);
            }
        }
    }
}