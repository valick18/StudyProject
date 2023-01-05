using StudyProject.Models;
using StudyProject.Models.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace StudyProject.Controllers
{
    [CustomAuthorize(UserRoles = new UserRole[] { UserRole.Admin, UserRole.Applicant, UserRole.Tutor })]
    public class HomeController : MainController
    {
        public ActionResult Index()
        {
            UserInfo uInfo = new UserInfo(db);
            tbUser user = uInfo.fuser;
            return View(user);
        }
    }
}