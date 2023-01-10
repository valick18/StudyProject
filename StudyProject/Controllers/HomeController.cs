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
            List<tbLesson> lessons = user.tbGroup.SelectMany(w => w.tbLesson).ToList();
            List<tbInstitution> institutions = lessons.Select(s=>s.tbTest).Select(s => s.tbInstitution).Distinct().ToList();
            ViewBag.institutions = institutions;

            return View(lessons);
        }
    }
}