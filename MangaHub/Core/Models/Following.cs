namespace MangaHub.Core.Models
{
    public class Following
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
    }
}