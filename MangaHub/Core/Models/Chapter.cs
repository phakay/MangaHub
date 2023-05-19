using MangaHub.Core.Enums;
using MangaHub.Core.Utitlity;
using System;
using System.Collections.Generic;
using System.Linq;

namespace MangaHub.Core.Models
{
    public class Chapter : INotify
    {
        public const char KeyCodeDelimiter = '_';
        public Manga Manga { get; set; }
        public int MangaId { get; set; }
        public int ChapterNo { get; set; }
        public int NumberOfPages { get; set; }
        public string Information { get; set; }
        public DateTime DateTime { get; private set; }

        public Chapter()
        {
            DateTime = DateTime.Now;
        }
        public string KeyCode
        { 
            get 
            {
                return $"{MangaId}{KeyCodeDelimiter}{ChapterNo}";
            } 
        }
        public void NotifyCreate()
        {
            var readers = Manga != null ? Manga.Readings.Select(r => r.User) : Enumerable.Empty<ApplicationUser>();
            AddNotification(NotificationType.Created, $"{Manga.Artist.Name} added Chapter {ChapterNo} for {Manga.Title}", readers);
        }

        public void NotifyDelete()
        {
            var readers = Manga != null ? Manga.Readings.Select(r => r.User) : Enumerable.Empty<ApplicationUser>();
            AddNotification(NotificationType.Deleted, $"{Manga.Artist.Name} removed Chapter {ChapterNo} for {Manga.Title}", readers);
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