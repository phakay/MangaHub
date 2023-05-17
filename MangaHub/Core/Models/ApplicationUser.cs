using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Security.Claims;
using System.Threading.Tasks;

namespace MangaHub.Core.Models
{
    // You can add profile data for the user by adding more properties to your ApplicationUser class, please visit https://go.microsoft.com/fwlink/?LinkID=317594 to learn more.
    public class ApplicationUser : IdentityUser
    {
        public string Name { get; set; }
        [JsonIgnore]
        public ICollection<UserNotification> UserNotifications { get; set; }
        [JsonIgnore]
        public ICollection<Following> Followers { get; set; }

        public ApplicationUser()
        {
            UserNotifications = new Collection<UserNotification>();
            Followers = new Collection<Following>();
        }
        public async Task<ClaimsIdentity> GenerateUserIdentityAsync(UserManager<ApplicationUser> manager)
        {
            // Note the authenticationType must match the one defined in CookieAuthenticationOptions.AuthenticationType
            var userIdentity = await manager.CreateIdentityAsync(this, DefaultAuthenticationTypes.ApplicationCookie);
            // Add custom user claims here
            return userIdentity;
        }

        public void Notify(Notification notification)
        {
            UserNotifications
                .Add(new UserNotification(this, notification));
        }
    }
}