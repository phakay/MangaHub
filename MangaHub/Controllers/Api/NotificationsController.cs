using MangaHub.Core;
using MangaHub.Core.Dtos;
using Microsoft.AspNet.Identity;
using System.Collections.Generic;
using System.Linq;
using System.Web.Http;
using WebGrease.Css.Extensions;

namespace MangaHub.Controllers.Api
{
    [Authorize]
    public class NotificationsController : ApiController
    {
        private readonly IUnitOfWork _uniOfWork;

        public NotificationsController(IUnitOfWork unitOfWork)
        {
            _uniOfWork = unitOfWork;
        }

        public IEnumerable<NotificationDto> GetNewNotifications()
        {
            var userId = User.Identity.GetUserId();
            var notifications = _uniOfWork.NotificationRepo.GetNotificationsForUser(userId);
            var result = from notification in notifications
                         select notification.GetNotificationMessage();
            return result;
        }

        [HttpPost]
        public IHttpActionResult MarkAsRead()
        {
            var notifications = _uniOfWork.UserNotificationRepo
                .GetUserNotifications(User.Identity.GetUserId());

            notifications.ForEach(n => n.Read());

            _uniOfWork.Complete();

            return Ok();
        }
    }
}
