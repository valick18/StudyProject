using StudyProject.Models;
using StudyProject.Models.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace StudyProject.Controllers.Tutor
{
    [CustomAuthorize(UserRoles = new UserRole[] { UserRole.Admin, UserRole.Tutor })]
    public class TutorController : MainController
    {

        public ActionResult Groups() {

            UserInfo uInfo = new UserInfo(db);
            List<tbInstitution> institutions = db.tbInstitution.ToList();
            institutions = institutions.Where(w => w.tbUser.Contains(uInfo.fuser)).ToList();
            return View(institutions);
        }

        [HttpPost]
        public ActionResult AddNewGroup(string Name, Guid idInstitution) {
           
            if (string.IsNullOrEmpty(Name))
                return RedirectToAction("Groups");

            tbInstitution inst = db.tbInstitution.Find(idInstitution);

            tbGroup newGroup = new tbGroup()
            {
                idGroup = Guid.NewGuid(),
                Name = Name,
                DateCreate = DateTime.Now,
            };
            db.tbGroup.Add(newGroup);
            inst.tbGroup.Add(newGroup);
            db.SaveChanges();


            return RedirectToAction("Groups");
        }

        [HttpPost]
        public ActionResult EditGroup(Guid idGroup, string Name)
        {

            if (string.IsNullOrEmpty(Name))
                return RedirectToAction("Groups");

            tbGroup group = db.tbGroup.Find(idGroup);
            group.Name = Name;
            db.SaveChanges();

            return RedirectToAction("Groups");
        }

        public ActionResult UsersGroup(Guid idGroup)
        {
            tbGroup group = db.tbGroup.Find(idGroup);
            List<tbUser> users = group.tbUser.ToList();
            ViewBag.Group = group;
            return View(users);
        }

        public ActionResult TutorInstitution() {

            UserInfo uInfo = new UserInfo(db);
            List<tbInstitution> institutions = uInfo.fuser.tbInstitution.ToList();
            return View(institutions);
        }

        public ActionResult TutorMaterials(Guid idInst)
        {
            UserInfo uInfo = new UserInfo(db);
            List<tbMaterials> materials = db.tbMaterials.ToList();
            materials = materials.Where(w => w.tbMaterial_Institution.id_institution.Equals(idInst)).ToList();
            ViewBag.idInstitution = idInst;
            return View(materials);
        }

        [ValidateInput(false)]
        [HttpPost]
        public ActionResult CreateMaterial(tbMaterials material, Guid idInst)
        {
          
            if (string.IsNullOrEmpty(material.Name) || string.IsNullOrEmpty(material.Text)) { 
               return RedirectToAction("TutorMaterials","Tutor", new { idInst = idInst });
            }
            UserInfo uInfo = new UserInfo(db);

            tbMaterials newMaterial = new tbMaterials()
            {
                idMaterial = Guid.NewGuid(),
                Name = material.Name,
                Text = material.Text,
                id_user = uInfo.idUser,
            };

            db.tbMaterials.Add(newMaterial);

            tbMaterial_Institution materialInst = new tbMaterial_Institution() { 
                id_institution = idInst,
                id_material = newMaterial.idMaterial
            };

            db.tbMaterial_Institution.Add(materialInst);

            db.SaveChanges();

            return RedirectToAction("TutorMaterials","Tutor", new { idInst = idInst });
        }


        public ActionResult ViewMateril(Guid idMaterial) {
            tbMaterials material = db.tbMaterials.Find(idMaterial);
            return View(material);
        }

        public ActionResult TutorTests(Guid idInst)
        {
            tbInstitution inst = db.tbInstitution.Find(idInst);
            List<tbTest> tests = inst.tbTest.ToList();
            ViewBag.idInstitution = idInst;
            return View(tests);
        }

        [HttpPost]
        public ActionResult CreateTest(string Name, Guid idInst) {

            if (!string.IsNullOrEmpty(Name)) {
                UserInfo uInfo = new UserInfo(db);

                tbTest test = new tbTest() { 
                    Name = Name,
                    idTest = Guid.NewGuid(),
                    id_institution = idInst,
                    id_user = uInfo.fuser.idUser,
                };
                tbInstitution institution = db.tbInstitution.Find(idInst);
                institution.tbTest.Add(test);
                db.SaveChanges();
            }

            return RedirectToAction("TutorTests", new { idInst = idInst});
        }


        public ActionResult ViewTest(Guid idTest)
        {
            tbTest test = db.tbTest.Find(idTest);

            List<SelectListItem> items = new List<SelectListItem>();
            items.Add(new SelectListItem { Text = TaskStuff.getNameType(TaskStuff.TaskType.SelectCheckBox), Value = "0", Selected = true });
            items.Add(new SelectListItem { Text = TaskStuff.getNameType(TaskStuff.TaskType.Input), Value = "1" });
            ViewBag.Type = items;

            return View(test);
        }

        [HttpPost]
        public ActionResult CreateTask(tbTask task, Guid idTest)
        {
            string[] onCheck = { task.Name, task.Description};

            if (!CheckField.checkOnNullOrEmpty(onCheck)) {

                if (task.Type == (int)TaskStuff.TaskType.SelectCheckBox && task.isManual) {
                    task.isManual = false;
                }

                tbTask newTask = new tbTask() { 
                    idTask = Guid.NewGuid(),
                    id_test = idTest,
                    Name = task.Name,
                    Audio = task.Audio,
                    Description = task.Description,
                    Picture = task.Picture,
                    Rate = task.Rate ?? 1,
                    isManual = task.isManual,
                    Type = task.Type,
                };
                db.tbTask.Add(newTask);
                db.SaveChanges();
            }

            return RedirectToAction("ViewTest", new { idTest = idTest });
        }

        public ActionResult ViewVariant(Guid idTask) {
            tbTask task = db.tbTask.Find(idTask);
            return View(task);
        }

        [HttpPost]
        public ActionResult CreateVariant(Guid idTask, tbTaskVariant variant) {
            if (!string.IsNullOrEmpty(variant.Name))
            {
                tbTask task = db.tbTask.Find(idTask);
                variant.idTaskVariant = Guid.NewGuid();
                db.tbTaskVariant.Add(variant);
                task.tbTaskVariant.Add(variant);
                db.SaveChanges();
            }
            return RedirectToAction("ViewVariant", new { idTask = idTask});
        }    
        
        [HttpPost]
        public ActionResult EditVariant(tbTaskVariant variant, Guid idTask) {
         
            tbTaskVariant oldVariant = db.tbTaskVariant.Find(variant.idTaskVariant);
       
            if (!string.IsNullOrEmpty(variant.Name))
            {
                oldVariant.Name = variant.Name;
            }
            
            oldVariant.isRight = variant.isRight;
            db.SaveChanges();

            return RedirectToAction("ViewVariant", new { idTask = idTask });
        }

        public ActionResult CreateInvite(Guid idGroup) {
          
            tbInvite invite = new tbInvite() { 
              idInvite = Guid.NewGuid(),
              id_group = idGroup
            };

            db.tbInvite.Add(invite);
            db.SaveChanges();

            return RedirectToAction("UsersGroup", new { idGroup = idGroup });
        }


        public ActionResult Lessons(Guid idGroup)
        {
            UserInfo uInfo = new UserInfo(db);
            tbGroup group = db.tbGroup.Find(idGroup);
            List<tbLesson> lessons = group.tbLesson.ToList();
           
            List<SelectListItem> itemsTest = new List<SelectListItem>();
           
            foreach (tbTest test in uInfo.fuser.tbTest) {
              if(!lessons.Where(w => w.tbTest.idTest == test.idTest).Any()){ 
                      itemsTest.Add(new SelectListItem { Text = test.Name, Value = test.idTest.ToString()});
                }
            }
            ViewBag.id_test = itemsTest;

            List<tbMaterials> materials = db.tbMaterials.Where(w => w.id_user == uInfo.idUser).ToList();

            List<SelectListItem> itemsMaterial = new List<SelectListItem>();
            foreach (tbMaterials material in materials)
            {
                itemsMaterial.Add(new SelectListItem { Text = material.Name, Value = material.idMaterial.ToString()});
            }

            ViewBag.id_material = itemsMaterial;
            ViewBag.idGroup = idGroup;

            return View(lessons);
        }

        [HttpPost]
        public ActionResult CreateLesson(tbLesson lesson, Guid idGroup) {
          if(!string.IsNullOrEmpty(lesson.Name))
            {
                tbLesson newLesson = new tbLesson()
                {
                    idLesson = Guid.NewGuid(),
                    Name = lesson.Name,
                    DateCreate = DateTime.Now,
                    id_material = lesson.id_material,
                    id_test = lesson.id_test
                };

                tbGroup group = db.tbGroup.Find(idGroup);
                group.tbLesson.Add(newLesson);
                db.SaveChanges();
            }
            return RedirectToAction("Lessons", new { idGroup = idGroup });
        }

    }
}