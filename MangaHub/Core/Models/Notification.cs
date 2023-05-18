using MangaHub.Core.Dtos;
using MangaHub.Core.Enums;
using Newtonsoft.Json;
using System;
using System.Linq;
using System.Reflection;
using System.Text;

namespace MangaHub.Core.Models
{
    public class Notification
    {
        public int Id { get; set; }
        public DateTime DateCreated { get; private set; }
        public NotificationType Type { get; set; }
        public byte[] DataBefore { get; set; }
        public byte[] DataAfter { get; set; }

        protected Notification()
        {
            DateCreated = DateTime.Now;
        }

        private Notification(NotificationType type, byte[] dataBefore, byte[] dataAfter):this()
        {
            Type = type;
            DataBefore = dataBefore;
            DataAfter = dataAfter;
        }

        public static Notification Add(NotificationType type, string dataBefore, string dataAfter)
        {
            var dataBeforeInBytes = (dataBefore != null && dataBefore.Length > 0) ? Encoding.ASCII.GetBytes(dataBefore) : new byte[0];
            var dataAfterInBytes = (dataAfter != null && dataAfter.Length > 0) ? Encoding.ASCII.GetBytes(dataAfter) : new byte[0];

            return new Notification(type, dataBeforeInBytes, dataAfterInBytes);
        }

        public NotificationDto GetNotificationMessage()
        {
            var message = string.Empty;
            if(Type == NotificationType.MangaCreated)
            {
                var manga = JsonConvert.DeserializeObject<Manga>(Encoding.ASCII.GetString(DataAfter));
                message = $"{manga.Title} has been uploaded by {manga.Artist.Name}.";
            }
            else if(Type == NotificationType.MangaUpdated)
            {
                var mangaBefore = JsonConvert.DeserializeObject<Manga>(Encoding.ASCII.GetString(DataBefore));
                var mangaAfter = JsonConvert.DeserializeObject<Manga>(Encoding.ASCII.GetString(DataAfter));
                if (mangaBefore == null || mangaAfter == null)
                    return new NotificationDto() 
                    {Message = string.Empty, DateCreated = DateCreated };

                var propertyNamestToCheck = new[] { nameof(Manga.Title), nameof(Manga.Description), nameof(Manga.GenreId), nameof(Manga.Picture) };
                var properties  =  typeof(Manga).GetProperties(BindingFlags.Public | BindingFlags.DeclaredOnly | BindingFlags.Instance)
                    .Where(p => propertyNamestToCheck.Contains(p.Name))
                    .ToArray();

                foreach(var property in properties)
                {
                    if(!IsEqual(property.PropertyType, property.GetValue(mangaBefore), property.GetValue(mangaAfter)))
                    {
                        var propName = property.Name;
                        message += $"{propName.Replace("Id", string.Empty)} was updated; ";
                    }
                }

            }
            else if(Type == NotificationType.MangaDeleted)
            {
                var mangaBefore = JsonConvert.DeserializeObject<Manga>(Encoding.ASCII.GetString(DataBefore));
                message = mangaBefore != null ? $"{mangaBefore.Title} has been deleted." : string.Empty;
            }
            else if(Type == NotificationType.ChapterDeleted)
            {
                var chapterBefore = JsonConvert.DeserializeObject<Chapter>(Encoding.ASCII.GetString(DataBefore));
                message = chapterBefore != null ? $"Chapter No. {chapterBefore.ChapterNo} " +
                    $"has been deleted for Manga: {chapterBefore.Manga.Title}." : string.Empty;
            }
            else if (Type == NotificationType.ChapterCreated)
            {
                var chapterAfter = JsonConvert.DeserializeObject<Chapter>(Encoding.ASCII.GetString(DataAfter));
                message = chapterAfter != null ? $"Chapter No. {chapterAfter.ChapterNo} " +
                    $"has been added for Manga: {chapterAfter.Manga.Title}." : string.Empty;
            }

            return new NotificationDto { Message = message, DateCreated = DateCreated };
        }
        private bool IsEqual(Type type, object obj1, object obj2)
        {
            if (obj1 == null && obj2 == null)
                return true;
            if (obj1 == null && obj2 != null)
                return false;
            if (obj1 != null && obj2 == null)
                return false;

            if(type == typeof(byte[]))
            {
                return (obj1 as byte[]).SequenceEqual(obj2 as byte[]);
            }

            return obj1.Equals(obj2);
        }
    }

} 