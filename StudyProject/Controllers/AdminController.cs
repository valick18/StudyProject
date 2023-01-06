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
            List<tbUser> users = db.tbUser.Where(w => w.Role == (int)UserRole.Admin || w.Role == (int)UserRole.Tutor).ToList();
            ViewBag.users = users;
            return View(institutions);
        }

        [HttpPost]
        public ActionResult AddNewInstitution(tbInstitution newInstitution, Guid[] users)
        {
            InstitutionBuilder.Build(db, newInstitution, users);
            return RedirectToAction("InstitutionManagement");
        }


        [HttpPost]
        public ActionResult EditInstitution(tbInstitution institution)
        {
            InstitutionBuilder.ReBuild(db, institution);
            return RedirectToAction("InstitutionManagement");
        }

        public ActionResult InstitutionUsers(Guid id)
        {
            tbInstitution institution = db.tbInstitution.Find(id);
            List<tbUser> users = institution.tbUser.ToList();
            List<tbUser> allTeachers = db.tbUser.Where(w => (w.Role == (int)UserRole.Admin || w.Role == (int)UserRole.Tutor)).ToList();
            allTeachers = allTeachers.Where(w => !users.Contains(w)).ToList();
            ViewBag.idInstitution = id;
            ViewBag.allTeachers = allTeachers;
            return View(users);
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

        //[HttpPost] 
        //public ActionResult RemoveInstitution(Guid idInst)
        //{
        //    tbInstitution inst = db.tbInstitution.Find(idInst);

        //    foreach (tbUser user in inst.tbUser) {
        //        inst.tbUser.Remove(user);
        //    }

        //    db.tbInstitution.Remove(inst);
        //    db.SaveChanges();
        //    return RedirectToAction("InstitutionManagement");
        //}

      
        public ActionResult RemoveUserFromInstitution(Guid idUser, Guid idInstitution)
        {
            tbInstitution inst = db.tbInstitution.Find(idInstitution);
            tbUser user = db.tbUser.Find(idUser);
            inst.tbUser.Remove(user);
            db.SaveChanges();
            return RedirectToAction("InstitutionUsers", new { id  = idInstitution});
        }

        public ActionResult AddUserToInstitution(Guid idInst, Guid[] users) {
            tbInstitution inst = db.tbInstitution.Find(idInst);
            foreach(Guid id in users)
            {
                tbUser user = db.tbUser.Find(id);
                inst.tbUser.Add(user);
            }
            db.SaveChanges();
            return RedirectToAction("InstitutionUsers", new { id = idInst });
        }

    }
}