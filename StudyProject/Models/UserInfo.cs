using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Web.Security;
using System.Security.Cryptography;
using System.Text;
using System.Text.RegularExpressions;
using System.Web;
using System.Web.Mvc;
using System.Data.Entity;
using System.Data;
using System.Security.Policy;
using System.Net.Mail;
using System.Data.Common;
using System.Data.SqlClient;
using System.Data.Entity.Core.Objects;
using System.Data.Entity.Infrastructure;
using System.Threading;
using System.ComponentModel.DataAnnotations;
using System.Net;
using ApplicationDbContext.Controllers;

namespace StudyProject.Models
{

    public class UserInfo
    {
        public tbUser fuser;
        private StudyModelEntitity db;

        public UserInfo(StudyModelEntitity db)
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

        public UserInfo(StudyModelEntitity db, string login)
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

    }
}
