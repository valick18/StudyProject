using System;
using System.Web;
using System.Web.Security;
using System.Security.Principal;
using System.Runtime.Serialization;
using ApplicationDbContext.Controllers;

namespace StudyProject.Models.Core
{

        [Serializable]
        public class UserIdentity : IIdentity, ISerializable
        {
            private System.Web.Security.FormsAuthenticationTicket ticket;
            private UserInfo user;

            public System.Web.Security.FormsAuthenticationTicket Ticket { get { return ticket; } }
            public UserIdentity(UserInfo uInfo)
            {
                if (uInfo == null)
                    throw new ArgumentException("account is null");
                user = uInfo;
                string dataTicket = user.fuser.idUser.ToString();

                ticket = new FormsAuthenticationTicket
                (
                  1,
                  dataTicket,
                  DateTime.Now,
                  DateTime.Now.AddMinutes(12 * 60),
                  true,
                  (user.fuser.LastName+ " "+ user.fuser.FirstName + " "+ user.fuser.MiddleName + " (" + user.fuser.Login.Trim() + ")").Trim(),
                  FormsAuthentication.FormsCookiePath
                );

                HttpCookie cookie = new HttpCookie(FormsAuthentication.FormsCookieName, FormsAuthentication.Encrypt(ticket));
                HttpContext.Current.Response.Cookies.Add(cookie);

            }


        public string AuthenticationType
            {
                get { return "Forms"; }
            }

            public bool IsAuthenticated
            {

                get
                {
                    bool AutorizationState = false;
                    Guid idUser;
                    try
                    {
                        using (StudyModelEntitity db = new StudyModelEntitity())
                        {

                            if (Guid.TryParse(ticket.Name, out idUser))
                            {
                                tbUser CurrentUserState = db.tbUser.Find(idUser);
                                if (CurrentUserState != null)
                                {
                                    AutorizationState = true;
                                }
                            }
                        }
                    }
                    finally
                    {
                        AutorizationState = false;
                    }
                    return AutorizationState;
                }
            }

            public string Name
            {
                get { return ticket.UserData; }
            }

            public Guid idUser
            {
                get { return user.fuser.idUser; }
            }

            public String DisplayName
            {
                get { if (user == null) return ""; else return ticket.UserData; }
            }
        public UserRole Role
        {
            get
            {
                return (UserRole)user.fuser.Role;
            }
        }

        public void GetObjectData(SerializationInfo info, StreamingContext context)
        {
            throw new NotImplementedException();
        }

        public void setHttpContextIdentity()
        {
            HttpContext.Current.User = new GenericPrincipal(new UserIdentity(user), null);
        }

    }
 }
