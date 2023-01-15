using StudyProject.Models;
using StudyProject.Models.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace StudyProject.Controllers
{
    [CustomAuthorize(UserRoles = new UserRole[] { UserRole.Admin, UserRole.Applicant, UserRole.Tutor })]
    public class HomeController : MainController
    {
        public ActionResult Index()
        {
            UserInfo uInfo = new UserInfo(db);
            tbUser user = uInfo.fuser;
            List<tbLesson> lessons = user.tbGroup.SelectMany(w => w.tbLesson).ToList();
            List<tbInstitution> institutions = lessons.Select(s=>s.tbTest).Select(s => s.tbInstitution).Distinct().ToList();
            ViewBag.institutions = institutions;
            return View(lessons);
        }

        public ActionResult Test(Guid id)
        {
            tbTest test = db.tbTest.Find(id);
            List<tbTask> tasks = test.tbTask.Where(w => w.tbTaskVariant.Any() || (w.Type == (int)TaskStuff.TaskType.Input && w.isManual)).ToList();
            ViewBag.test = test;
            ViewBag.tasks = tasks;
            return View();
        }
        public ActionResult TestResult(Guid idTest)
        {
            UserInfo uInfo = new UserInfo(db);
            tbTest test = db.tbTest.Find(idTest);
            List<tbTaskResult> userResult = test.tbTaskResult.Where(w => w.id_user == uInfo.idUser).ToList();
            var userResultsByDate = userResult.GroupBy(g => g.TimeCreate).ToList();
            int maxRate = (int)test.tbTask.Where(w => w.tbTaskVariant.Any() || (w.Type == (int)TaskStuff.TaskType.Input && w.isManual)).Sum(s => s.Rate);
            ViewBag.maxRate = maxRate;
            ViewBag.test = test;
            return View(userResultsByDate);
        }

        public ActionResult TestRate() {
            UserInfo uInfo = new UserInfo(db);
            IEnumerable<tbTaskResult> taskResults = db.tbTaskResult.Where(w => w.id_user == uInfo.idUser);
            List<tbTest> tests = taskResults.Select(s => s.tbTest).Distinct().ToList();
            return View(tests);
        }


        [HttpPost]
        public ActionResult Test(List<TaskAnswer> userAnswers, Guid idTest) {
            DateTime timeNow = DateTime.Now;
            TaskResultBuilder resultBuilder = new TaskResultBuilder(db);
            List<tbTaskVariant> variants = new List<tbTaskVariant>();
            tbTest test = db.tbTest.Find(idTest);
            foreach (TaskAnswer uAnswer in userAnswers)
            {
                tbTask task = db.tbTask.Find(uAnswer.idTask);
                if (task.Type == (int)TaskStuff.TaskType.Input && task.isManual)
                {
                    if (string.IsNullOrEmpty(uAnswer.answer)) {
                        uAnswer.answer = "";
                    }
                    resultBuilder.Build(task.idTask, uAnswer.idTest, uAnswer.answer, timeNow, null);
                }
                else if (task.Type == (int)TaskStuff.TaskType.Input)
                {
                    int? Rate = 0;

                    if (string.IsNullOrEmpty(uAnswer.answer)) {
                        uAnswer.answer = "";
                    }

                    foreach (string variantAnswer in task.tbTaskVariant.Select(s => s.Name))
                    {
                        if (uAnswer.answer.ToLower().Equals(variantAnswer.ToLower()))
                        {
                            Rate = task.Rate;
                        }
                    }

                    if (Rate == null)
                    {
                        Rate = 0;
                    }

                    resultBuilder.Build(task.idTask, (Guid)task.id_test, uAnswer.answer, timeNow, Rate);
                }
                else {
                    int count = 0;
                    string userAnswer = "";
                    foreach (SelectedId sId in uAnswer.SelectedIds) {
                        tbTaskVariant variant = db.tbTaskVariant.Find(sId.idTaskVariant);
                        if (variant.isRight && sId.isSelected)
                        {
                            userAnswer += variant.Name + " ";
                            count++;
                        }
                        if (!variant.isRight  && sId.isSelected) {
                            count = 0;
                            break;
                        }

                    }
                    int maxRightAnswer = task.tbTaskVariant.Where(w => w.isRight).Count();

                    if (maxRightAnswer == count)
                    {
                        resultBuilder.Build(task.idTask, (Guid)task.id_test, userAnswer, timeNow, task.Rate);
                    }
                    else
                    {
                        resultBuilder.Build(task.idTask, (Guid)task.id_test, userAnswer, timeNow, 0);
                    }

                }
              
            }
            test.Attempt = test.Attempt - 1;
            db.SaveChanges();
            return RedirectToAction("TestResult", new { idTest = idTest });
        }

        public ActionResult Material(Guid id) {
            tbMaterials material = db.tbMaterials.Find(id);
            return View(material);
        }

        public ActionResult Materials()
        {
            UserInfo uInfo = new UserInfo(db);
            List<tbGroup> groups = uInfo.fuser.tbGroup.ToList();
            List<tbInstitution> institutions = groups.SelectMany(s => s.tbInstitution).ToList();
            return View(institutions);
        }

        public ActionResult EditUser() {
            UserInfo uInfo = new UserInfo(db);
            tbUser user = uInfo.fuser;
            return View(user);
        }

        [HttpPost]
        public ActionResult EditUser(tbUser user) {
            UserInfo uInfo = new UserInfo(db);
            tbUser oldUser = uInfo.fuser;
            if (!string.IsNullOrEmpty(user.FirstName)) {
                oldUser.FirstName = user.FirstName;
            }
            if (!string.IsNullOrEmpty(user.LastName)) {
                oldUser.LastName = user.LastName;
            }
            if (!string.IsNullOrEmpty(user.MiddleName)) {
                oldUser.MiddleName = user.MiddleName;
            }

            if (user.Age != null && user.Age != 0) {
                oldUser.Age = user.Age;
            }
            db.SaveChanges();
            return View(oldUser);
        }

        [HttpPost]
        public ActionResult EditPhoto(HttpPostedFileBase LoadUserPic) {
            if (LoadUserPic != null) {
                UserInfo uInfo = new UserInfo(db);
                var binaryData = new byte[LoadUserPic.ContentLength];
                LoadUserPic.InputStream.Read(binaryData, 0, LoadUserPic.ContentLength);
                uInfo.fuser.Photo = binaryData;
                db.SaveChanges();
            }
            return RedirectToAction("EditUser", "Home");
        }

        [HttpPost]
        public ActionResult RemovePhoto()
        {
            UserInfo uInfo = new UserInfo(db);
            uInfo.fuser.Photo = null;
            db.SaveChanges();
            return RedirectToAction("EditUser");
        }

    }
}