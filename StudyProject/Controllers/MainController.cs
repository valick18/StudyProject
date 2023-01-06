using System.Web.Mvc;

namespace StudyProject
{
    public class MainController : Controller
    {
        public StudyProject.StudyPlatformEntities db;

        public MainController()
        {
            db = new StudyPlatformEntities();
        }

    }
}