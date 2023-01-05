using System.Web.Mvc;


namespace StudyProject.Controllers
{
    public class MainController : Controller
    {
        public StudyPlatformEntities db;

        public MainController()
        {
            db = new StudyPlatformEntities();
        }

    }
}