using MangaHub.Core;
using MangaHub.Core.Dtos;
using MangaHub.Core.Models;
using MangaHub.Utility;
using Microsoft.AspNet.Identity;
using System.Linq;
using System.Web.Http;

namespace MangaHub.Controllers.Api
{
    public class FollowingsController : ApiController
    {
        private readonly IUnitOfWork _unitOfWork;

        public FollowingsController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
         }

        [HttpPost, Authorize(Roles ="Reader")]
        public IHttpActionResult Follow(FollowingDto dto)
        {
            if (!ModelState.IsValid)
            {
                var errorMessage = string.Join(" | ", ModelState.Values
                .SelectMany(v => v.Errors)
                .Select(e => e.ErrorMessage));
                return BadRequest(errorMessage);
            }

            if (dto is null)
                return BadRequest();

            if (_unitOfWork.UserRepo.GetUser(dto.FolloweeId) == null)
                return BadRequest("FolloweeId does not exist");

            var userId = User.Identity.GetUserId();

            if (_unitOfWork.FollowingRepo.GetFollowing(userId, dto.FolloweeId) != null)
                return BadRequest("Following already exists");

            var follower = _unitOfWork.UserRepo.GetUser(userId);
            var followee = _unitOfWork.UserRepo.GetUser(dto.FolloweeId);

            var following = new Following(follower, followee);

            var notifier = new Notifier();
            var notificationMessage = $"{follower.Name} is following you";
            notifier.NotifyCreate(new[] { followee }, notificationMessage);

            _unitOfWork.FollowingRepo.Add(following);
            _unitOfWork.Complete();

            return Ok();
        }

        [HttpDelete, Authorize(Roles = "Reader")]
        public IHttpActionResult Unfollow(string id)
        {
            var following = _unitOfWork.FollowingRepo.GetFollowingWithUsers(User.Identity.GetUserId(), id);

            if (following == null)
                return NotFound();

            var notifier = new Notifier();
            var notificationMessage = $"{following.Follower.Name} has unfollowed you";
            notifier.NotifyDelete(new[] { following.Followee }, notificationMessage);

            _unitOfWork.FollowingRepo.Remove(following);
            _unitOfWork.Complete();

            return Ok();
        }
    }
}