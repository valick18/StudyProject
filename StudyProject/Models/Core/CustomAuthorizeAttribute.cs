using System;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using System.Web.Routing;
using System.Linq;
using ApplicationDbContext.Controllers;

namespace StudyProject.Models.Core
{
    public class CustomAuthorizeAttribute : AuthorizeAttribute
    {
        public UserRole[] UserRoles;
        protected override bool AuthorizeCore(HttpContextBase httpContext)
        {
            if (httpContext == null)
                throw new ArgumentNullException("httpContext");

            if (!httpContext.User.Identity.IsAuthenticated)
                return false;
            UserRole role = UserRole.Anonim;
            if (HttpContext.Current.User.Identity.IsAuthenticated)
            {
                FormsIdentity fi = ((FormsIdentity)HttpContext.Current.User.Identity);
                using (StudyModelEntitity db = new StudyModelEntitity())
                {
                    tbUser usr = db.tbUser.Find(new Guid(fi.Ticket.Name));
                    if (usr != null)
                    {
                        role = (UserRole)usr.Role;
                    }
                }

            }
            if (UserRoles != null)
            {
                return UserRoles.Where(w => w == role).Any();
            }
            return true;
        }

        protected override void HandleUnauthorizedRequest(AuthorizationContext filterContext)
        {
            filterContext.Result = new RedirectToRouteResult(
            new RouteValueDictionary{
                { "action", "Login" },
                { "controller", "EnterSystem" },
            });
        }
    }
}