using StudyProject.Models;
using StudyProject.Models.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace StudyProject.Controllers
{
    [CustomAuthorize(UserRoles = new UserRole[] { UserRole.Admin })]
    public class AdminController : MainController
    {
        public ActionResult UserManagement()
        {
            List<tbUser> users = db.tbUser.ToList();
            return View(users);
        }


        public ActionResult InstitutionManagement() {
            List<tbInstitution> institutions = db.tbInstitution.ToList();
            return View(institutions);
        }

        public ActionResult AddNewInstitution() {
            return View();
        }

        [HttpPost]
        public ActionResult AddNewInstitution(tbInstitution newInstitution)
        {
            InstitutionBuilder.Build(db, newInstitution);
            return RedirectToAction("InstitutionManagement");
        }

        public ActionResult EditInstitution(Guid idInstitution) {
            tbInstitution inst = db.tbInstitution.Find(idInstitution);
            return View(inst);
        }

        [HttpPost]
        public ActionResult EditInstitution(tbInstitution institution)
        {
            InstitutionBuilder.ReBuild(db, institution);
            return RedirectToAction("InstitutionManagement");
        }

        [HttpPost]
        public ActionResult ChangeUserRole(Guid idUser, int Role)
        {
            tbUser user = db.tbUser.Find(idUser);
            switch ((UserRole)Role)
            {
                case UserRole.Admin:
                    user.Role = (int)UserRole.Admin;
                    break;
                case UserRole.Tutor:
                    user.Role = (int)UserRole.Tutor;
                    break;
                case UserRole.Applicant:
                default:
                    user.Role = (int)UserRole.Applicant;
                    break;
            };
            db.SaveChanges();
            return RedirectToAction("UserManagement");
        }

        public ActionResult RemoveUser(Guid idUser)
        {
            tbUser user = db.tbUser.Find(idUser);
            db.tbUser.Remove(user);
            db.SaveChanges();
            return RedirectToAction("UserManagement");
        }

    }
}