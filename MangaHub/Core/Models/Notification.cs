using MangaHub.Core.Enums;
using System;

namespace MangaHub.Core.Models
{
    public class Notification
    {
        public int Id { get; set; }
        public DateTime DateCreated { get; private set; }
        public NotificationType Type { get; set; }
        public string Message { get; set; }

        protected Notification()
        {
            DateCreated = DateTime.Now;
        }

        private Notification(NotificationType type, string message ):this()
        {
            Type = type;
            Message = message;
        }

        public static Notification Add(NotificationType type, string message)
        {
            return new Notification(type, message);
        }

    }

} 