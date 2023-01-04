using ApplicationDbContext.Controllers;
using StudyProject.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace StudyProject.Controllers
{
    public class MainController : Controller
    {
        public StudyModelEntitity db;

        public MainController() { 
            db = new StudyModelEntitity();
        }

    }
}