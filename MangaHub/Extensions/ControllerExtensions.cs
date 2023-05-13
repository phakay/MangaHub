using Moq;
using System.Security.Claims;
using System.Security.Principal;
using System.Web;
using System.Web.Mvc;

namespace MangaHub.Extensions
{
    public static class ControllerExtensions
    {
        public static void MockCurrentUser(this Controller controller, string userId, string username)
        {
            var identity = new GenericIdentity(username);

            identity.AddClaim(
                new Claim("http://schemas.xmlsoap.org/ws/2005/05/identity/claims/name", username)
                );

            identity.AddClaim(
                new Claim("http://schemas.xmlsoap.org/ws/2005/05/identity/claims/nameidentifier", userId)
                );

            var principal = new GenericPrincipal(identity, null);

            controller.ControllerContext = Mock.Of<ControllerContext>(c => 
                c.HttpContext == Mock.Of<HttpContextBase>(http =>
                    http.User == principal));            
        }
    }
}
