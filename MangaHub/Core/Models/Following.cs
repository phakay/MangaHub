using MangaHub.Core.Enums;
using MangaHub.Core.Utitlity;
using System.Collections.Generic;

namespace MangaHub.Core.Models
{
    public class Following : INotify
    {
        public string FollowerId { get; set; }
        public string FolloweeId { get; set; }
        public ApplicationUser Follower { get; set; }
        public ApplicationUser Followee { get; set; }

        public Following()
        {}
        public Following(ApplicationUser follower, ApplicationUser followee)
        {
            Follower = follower ?? throw new System.ArgumentNullException(nameof(follower));
            Followee = followee ?? throw new System.ArgumentNullException(nameof(followee));
        }
        public void NotifyCreate()
        {
            AddNotification(NotificationType.Created, $"{Follower.Name} is following you", new[] { Followee });
        }

        public void NotifyDelete()
        {
            AddNotification(NotificationType.Deleted, $"{Follower.Name} has unfollowed you", new[] { Followee });
        }

        public void AddNotification(NotificationType notificationType, string message, IEnumerable<ApplicationUser> usersToNotify)
        {
            var notification = Notification.Add(notificationType, message);
            foreach (var user in usersToNotify)
            {
                user.Notify(notification);
            }
        }
    }
}