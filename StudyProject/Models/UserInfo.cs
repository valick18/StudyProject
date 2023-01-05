using System;
using System.Linq;
using System.Web.Security;
using System.Web;
using StudyProject.Models.Core;

namespace StudyProject.Models
{

    public class UserInfo
    {
        public tbUser fuser;
        private StudyPlatformEntities db;

        public UserInfo(StudyPlatformEntities db)
        {
            this.db = db;
            //умова не виконується, оскільки Identity не встановлене
            if (HttpContext.Current.User.Identity.IsAuthenticated)
            {
                FormsIdentity fi = (FormsIdentity)HttpContext.Current.User.Identity;
                Guid IdUser = new Guid(fi.Ticket.Name);
                fuser = db.tbUser.Find(IdUser);
            }
            else
            {
                fuser = null;
            }
        }

        public UserInfo(StudyPlatformEntities db, string login)
        {
            this.db = db;
            if (!string.IsNullOrEmpty(login))
            {
                fuser = db.tbUser.FirstOrDefault(w => w.Login.Equals(login));
            }
            else
            {
                fuser = null;
            }
        }

        public static string getTextByRole(UserRole role)
        {
            switch (role)
            {
                case UserRole.Admin:
                    return "Адмін";
                case UserRole.Applicant:
                    return "Учень";
                case UserRole.Tutor:
                    return "Викладач";
                case UserRole.Anonim:
                default:
                    return "Невідомий";
            };
        }

    }
}
