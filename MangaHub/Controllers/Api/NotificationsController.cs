using AutoMapper;
using MangaHub.Core;
using MangaHub.Core.Dtos;
using MangaHub.Core.Models;
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
            var notifications = _uniOfWork.NotificationRepo
                .GetNotificationsForUser(User.Identity.GetUserId());

            return notifications.Select(Mapper.Map<Notification, NotificationDto>);
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
