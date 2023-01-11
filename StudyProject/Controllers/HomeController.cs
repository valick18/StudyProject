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
            return View(test);
        }
        [HttpPost]
        public ActionResult Test(Guid [] idTaskVariants, string [] inputAnswers, Guid [] answersId, Guid idTest)
        {
            UserInfo uInfo = new UserInfo(db);
            DateTime timeNow = DateTime.Now;
            for (int i = 0; i < idTaskVariants.Count(); i++) {
                Guid id = idTaskVariants.ElementAt(i);
                string answer = inputAnswers.ElementAt(i);
                tbTaskVariant variant = db.tbTaskVariant.Find(id);
             
                if (variant.tbTask.isManual)
                {
                    tbTaskResult result = new tbTaskResult() {
                        idTaskResult = Guid.NewGuid(),
                        id_task = variant.id_task,
                        id_user = uInfo.idUser,
                        id_test = (Guid)variant.tbTask.id_test,
                        Rate = null,
                        UserAnswer = answer,
                        TimeCreate = timeNow
                    };
                    db.tbTaskResult.Add(result);
                }
                else {
                    tbTaskResult result = new tbTaskResult()
                    {
                        idTaskResult = Guid.NewGuid(),
                        id_task = variant.id_task,
                        id_user = uInfo.idUser,
                        id_test = (Guid)variant.tbTask.id_test,
                        UserAnswer = answer,
                        TimeCreate = timeNow
                    };

                    if (answer.ToLower().Equals(variant.Name.ToLower()))
                    {
                        result.Rate = variant.tbTask.Rate;
                    }
                    else { 
                        result.Rate = 0;
                    }
                    db.tbTaskResult.Add(result);
                }
            }

            List<tbTaskVariant> variants = new List<tbTaskVariant>();
            List<Guid> idTasks = new List<Guid>();

            foreach (Guid id in answersId) {
                tbTaskVariant variant = db.tbTaskVariant.Find(id);
                if (!idTasks.Contains(variant.id_task)) { 
                      variants.Add(variant);
                      idTasks.Add(variant.id_task);
                }
            }

            //Додаю результат задачі по чекбоксам
            foreach (tbTaskVariant variant in variants) {
                if (variant.isRight)
                {
                    tbTaskResult result = new tbTaskResult()
                    {
                        idTaskResult = Guid.NewGuid(),
                        id_task = variant.id_task,
                        id_user = uInfo.idUser,
                        id_test = (Guid)variant.tbTask.id_test,
                        UserAnswer = variant.Name,
                        Rate = variant.tbTask.Rate,
                        TimeCreate = timeNow
                    };
                    db.tbTaskResult.Add(result);
                }
                else {
                    tbTaskResult result = new tbTaskResult()
                    {
                        idTaskResult = Guid.NewGuid(),
                        id_task = variant.id_task,
                        id_user = uInfo.idUser,
                        id_test = (Guid)variant.tbTask.id_test,
                        UserAnswer = variant.Name,
                        Rate = 0,
                        TimeCreate = timeNow
                    };
                    db.tbTaskResult.Add(result);
                }
            }

            tbTest test = db.tbTest.Find(idTest);
            test.Attempt = test.Attempt - 1;
            db.SaveChanges();
            return RedirectToAction("TestResult", new { idTest = idTest});
        }

        public ActionResult TestResult(Guid idTest)
        {
            UserInfo uInfo = new UserInfo(db);
            tbTest test = db.tbTest.Find(idTest);
            List<tbTaskResult> userResult = test.tbTaskResult.Where(w => w.id_user == uInfo.idUser).ToList();
            var userResultsByDate = userResult.GroupBy(g => g.TimeCreate).ToList();
            return View(userResultsByDate);
        }
    }
}