using MangaHub.Core.Enums;
using System;

namespace MangaHub.Core.Models
{
    public class Notification
    {
        public int Id { get; set; }
        public DateTime DateCreated { get; }
        public NotificationType Type { get; set; }
        public byte[] DataBefore { get; set; }
        public byte[] DataAfter { get; set; }

        public Notification()
        {
            DateCreated = DateTime.Now;
        }
    }
}