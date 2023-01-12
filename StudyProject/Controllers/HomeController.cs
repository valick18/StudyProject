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
            tbTest test = db.tbTest.Find(idTest);
            TaskResultBuilder resBuilder = new TaskResultBuilder(db);


            if (test.Attempt <= 0) { 
                return RedirectToAction("TestResult", new { idTest = idTest});
            }

            UserInfo uInfo = new UserInfo(db);
            DateTime timeNow = DateTime.Now;

            for (int i = 0; i < idTaskVariants.Count(); i++) {
                Guid id = idTaskVariants.ElementAt(i);
                string answer = inputAnswers.ElementAt(i);
                tbTaskVariant variant = db.tbTaskVariant.Find(id);
             
                if (variant.tbTask.isManual)
                {
                    resBuilder.Build(variant.id_task, (Guid)variant.tbTask.id_test, answer, timeNow, null);
                }
                else {

                    int? Rate = 0;
                    foreach (string variantAnswer in variant.tbTask.tbTaskVariant.Select(s => s.Name)) {
                        if (answer.ToLower().Equals(variantAnswer.ToLower()))
                        {
                            Rate = variant.tbTask.Rate;
                        }
                    }

                    if (Rate == null) {
                        Rate = 0;
                    }
                    resBuilder.Build(variant.id_task, (Guid)variant.tbTask.id_test, answer, timeNow, Rate);
                }
            }

            if (answersId != null)
            {
                List<tbTaskVariant> variants = new List<tbTaskVariant>();

                foreach (Guid id in answersId) {
                    tbTaskVariant variant = db.tbTaskVariant.Find(id);
                    variants.Add(variant);
                }

                var groupedVariantsByTask = variants.GroupBy(g => g.id_task).ToList();

                foreach (var group in groupedVariantsByTask)
                {
                    tbTask task = db.tbTask.Find(group.Key);
                    int mustBeRightAnswer = task.tbTaskVariant.Where(w => w.isRight).Count();
                    int countRightAnswer = 0;
                    string userAnswer = "";
                    foreach (tbTaskVariant variant in group) {
                        if (variant.isRight) {
                            countRightAnswer++;
                        }
                        userAnswer += variant.Name + ", ";
                    }

                    if (mustBeRightAnswer == countRightAnswer)
                    {
                        resBuilder.Build(task.idTask, (Guid)task.id_test, userAnswer, timeNow, task.Rate);
                    }
                    else {
                        resBuilder.Build(task.idTask, (Guid)task.id_test, userAnswer, timeNow, 0);
                    }

                }
            }

            test.Attempt = test.Attempt - 1;
            db.SaveChanges();
            return RedirectToAction("TestResult", new { idTest = idTest});
        }

        public ActionResult TestResult(Guid idTest)
        {
            //Ще відобразити кінцеву оцінку/ без ручного ввода(Написати що ше є завдання на перевірці й кінцевий бал може змінитись) 
            UserInfo uInfo = new UserInfo(db);
            tbTest test = db.tbTest.Find(idTest);
            List<tbTaskResult> userResult = test.tbTaskResult.Where(w => w.id_user == uInfo.idUser).ToList();
            var userResultsByDate = userResult.GroupBy(g => g.TimeCreate).ToList();
            return View(userResultsByDate);
        }
    }
}