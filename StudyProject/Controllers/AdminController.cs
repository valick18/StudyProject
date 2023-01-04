using ApplicationDbContext.Controllers;
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
        // GET: Admin
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult UserManagement() {
            List<tbUser> users = db.tbUser.ToList();
            return View(users);

        }
        [HttpPost]
        public ActionResult AddNewAdmin(Guid idUser) {
         
            tbUser user = db.tbUser.Find(idUser);
            user.Role = (int)UserRole.Admin;
            db.SaveChanges();

            return RedirectToAction("UserManagement");
        }

        public ActionResult RemoveUser(Guid idUser) {
            tbUser user = db.tbUser.Find(idUser);
            db.tbUser.Remove(user);
            db.SaveChanges();
            return RedirectToAction("UserManagement");
        }

    }
}