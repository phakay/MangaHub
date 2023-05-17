using MangaHub.Core.Enums;
using MangaHub.Core.Utitlity;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;

namespace MangaHub.Core.Models
{
    public class Manga : INotify
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
        [JsonIgnore]
        public ICollection<Chapter> Chapters { get; private set; }
        [JsonIgnore]
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
            var dataBefore = JsonConvert.SerializeObject(this);
            Update(objToUpdate);
            var dataAfter = JsonConvert.SerializeObject(this);
            AddNotification(NotificationType.MangaUpdated, dataBefore, dataAfter, Readings.Select(r => r.User));
        }

        public void NotifyCreate()
        {
            var dataBefore = string.Empty;
            var dataAfter = JsonConvert.SerializeObject(this);
            var followers = Artist != null ? Artist.Followers.Select(f => f.Follower) : Enumerable.Empty<ApplicationUser>();
            AddNotification(NotificationType.MangaCreated, dataBefore, dataAfter, followers);
        }

        public void NotifyDelete()
        {
            var dataBefore = JsonConvert.SerializeObject(this);
            var dataAfter = String.Empty;
            AddNotification(NotificationType.MangaDeleted, dataBefore, dataAfter, Readings.Select(r => r.User));
        }

        public void AddNotification(NotificationType notificationType, string dataBefore, string dataAfter, IEnumerable<ApplicationUser> users)
        {
            var notification = Notification.Add(notificationType, dataBefore, dataAfter);
            foreach (var user in users)
            {
                user.Notify(notification);
            }
        }
    }
}