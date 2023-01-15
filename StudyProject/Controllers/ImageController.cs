using StudyProject.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace StudyProject.Controllers
{
    public class ImageController : MainController
    {
        public ActionResult GetUserImage()
        {
            UserInfo uInfo = new UserInfo(db);
            tbUser user = uInfo.fuser;
            if (user == null)
                return null;
            byte[] imageData = user.Photo;
            if (imageData == null)
            {
                return null;
            }
            return File(imageData, "image/png");
        }
    }
}