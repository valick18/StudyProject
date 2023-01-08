using IdentityModel;
using Regexs;
using StudyProject.Controllers;
using StudyProject.Models;
using StudyProject.Models.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Principal;
using System.Web;
using System.Web.Mvc;
using StudyProject;
using System.Web.Security;

namespace StudingPlatform.Controllers
{
    public class EnterSystemController : MainController
    {
        [HttpGet]
        public ActionResult Login(Guid? idInvite)
        {
            ViewBag.idInvite = idInvite;
            return View();
        }

        [HttpPost]
        public ActionResult Login(string login, string password, Guid? idInvite)
        {
            if (string.IsNullOrEmpty(login) || string.IsNullOrEmpty(password))
            {
                return View();
            }

            tbUser user = db.tbUser.Where(w => w.Login.Equals(login)).FirstOrDefault();
            PassCoder coder = new PassCoder(user);
            if (coder.VerifyPassword(password))
            {
                UserInfo uInfo = new UserInfo(db, login);
                HttpContext.User = new GenericPrincipal(new UserIdentity(uInfo), null);

                if (idInvite != null) {
                    tbInvite invite = db.tbInvite.Find(idInvite);
                    if (!invite.tbGroup.tbUser.Contains(user)) {
                        invite.tbGroup.tbUser.Add(user);
                        db.SaveChanges();
                    }
                }

                return RedirectToAction("Index", "Home");
            }
            return View();
        }


        public ActionResult Register(Guid? idInvite)
        {
            ViewBag.idInvite = idInvite;
            return View();
        }

        [HttpPost]
        public ActionResult Register(RegistrationModel newUser, Guid? idInvite)
        {
            //урахувати дублювання емайлів
            bool isValidEmail = RegexUtilities.IsValidEmail(newUser.Login);
            bool isNotNullOrEmptyFields = !string.IsNullOrEmpty(newUser.FirstName) && !string.IsNullOrEmpty(newUser.LastName) && !string.IsNullOrEmpty(newUser.MiddleName) && !string.IsNullOrEmpty(newUser.Password);
            bool isEqualPass = newUser.RepeatPassword != null && newUser.Password.Equals(newUser.RepeatPassword);
            if (isValidEmail && isNotNullOrEmptyFields && newUser.Age >= 12 && isEqualPass)
            {
                UserBuilder uBuilder = new UserBuilder(newUser);
                tbUser user = uBuilder.Build();
                db.tbUser.Add(user);
                db.SaveChanges();

                UserInfo uInfo = new UserInfo(db, newUser.Login);
                HttpContext.User = new GenericPrincipal(new UserIdentity(uInfo), null);

                if (idInvite != null)
                {
                    tbInvite invite = db.tbInvite.Find(idInvite);
                    invite.tbGroup.tbUser.Add(user);
                    db.SaveChanges();
                }

                return RedirectToAction("Index", "Home");
            }
            return View();
        }

        public ActionResult Logout() {
           
            Response.Cache.SetCacheability(HttpCacheability.NoCache);
            Session.Abandon();
            Session.Clear();
            FormsAuthentication.SignOut();

            return RedirectToAction("Login","EnterSystem");
        }

        public ActionResult Invite(Guid id) {

            ViewBag.idInvite = id;

            return View();
        }

    }
}