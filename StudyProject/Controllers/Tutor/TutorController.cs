using StudyProject.Models;
using StudyProject.Models.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace StudyProject.Controllers.Tutor
{
    [CustomAuthorize(UserRoles = new UserRole[] { UserRole.Admin, UserRole.Tutor })]
    public class TutorController : MainController
    {

        public ActionResult Groups() {

            UserInfo uInfo = new UserInfo(db);
            List<tbInstitution> institutions = db.tbInstitution.ToList();
            institutions = institutions.Where(w => w.tbUser.Contains(uInfo.fuser)).ToList();
            return View(institutions);
        }

        [HttpPost]
        public ActionResult AddNewGroup(string Name, Guid idInstitution) {
           
            if (string.IsNullOrEmpty(Name))
                return RedirectToAction("Groups");

            tbInstitution inst = db.tbInstitution.Find(idInstitution);

            tbGroup newGroup = new tbGroup()
            {
                idGroup = Guid.NewGuid(),
                Name = Name,
                DateCreate = DateTime.Now,
            };
            db.tbGroup.Add(newGroup);
            inst.tbGroup.Add(newGroup);
            db.SaveChanges();


            return RedirectToAction("Groups");
        }

        [HttpPost]
        public ActionResult EditGroup(Guid idGroup, string Name)
        {

            if (string.IsNullOrEmpty(Name))
                return RedirectToAction("Groups");

            tbGroup group = db.tbGroup.Find(idGroup);
            group.Name = Name;
            db.SaveChanges();

            return RedirectToAction("Groups");
        }

        public ActionResult UsersGroup(Guid idGroup)
        {
            tbGroup group = db.tbGroup.Find(idGroup);
            List<tbUser> users = group.tbUser.ToList();
            return View(users);
        }

    }
}