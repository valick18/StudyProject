using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace StudyProject.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            var db = new ContextEntitity();
            //string name = db.tbInstitution.FirstOrDefault().DateCreate.ToString();
            //ViewBag.name = name;
            return View();
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
    }
}