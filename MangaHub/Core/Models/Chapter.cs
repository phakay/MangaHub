using MangaHub.Core.Enums;
using MangaHub.Core.Utitlity;
using Newtonsoft.Json;
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
            var dataBefore = string.Empty;
            var dataAfter = JsonConvert.SerializeObject(this);
            var readers = Manga != null ? Manga.Readings.Select(r => r.User) : Enumerable.Empty<ApplicationUser>();
            AddNotification(NotificationType.ChapterCreated, dataBefore, dataAfter, readers);
        }

        public void NotifyDelete()
        {
            var dataBefore = JsonConvert.SerializeObject(this);
            var dataAfter = String.Empty;
            var readers = Manga != null ? Manga.Readings.Select(r => r.User) : Enumerable.Empty<ApplicationUser>();
            AddNotification(NotificationType.ChapterDeleted, dataBefore, dataAfter, readers);
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