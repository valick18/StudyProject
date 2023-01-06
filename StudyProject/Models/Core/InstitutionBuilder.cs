using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace StudyProject.Models.Core
{
    public class InstitutionBuilder
    {

        public static void Build(StudyPlatformEntities db, tbInstitution institution) {
            UserInfo uInfo = new UserInfo(db);
            tbInstitution newInstitution = new tbInstitution()
            {
                idInstitution = Guid.NewGuid(),
                id_user = uInfo.idUser,
                Name = institution.Name,
                Adress = institution.Adress,
                DateCreate = DateTime.Now,
                //Logo = institution.Logo,
            };
            db.tbInstitution.Add(newInstitution);
            db.SaveChanges();
        }

        public static void ReBuild(StudyPlatformEntities db, tbInstitution newInstitution)
        {
            tbInstitution institution = db.tbInstitution.Find(newInstitution.idInstitution);
            institution.Name = newInstitution.Name.TrimEnd();
            institution.Adress = newInstitution.Adress.TrimEnd();
            //institution.Logo = newInstitution.Logo,
            db.SaveChanges();
        }

        public static void Build(StudyPlatformEntities db, tbInstitution institution, Guid [] users)
        {
            UserInfo uInfo = new UserInfo(db);
            tbInstitution newInstitution = new tbInstitution()
            {
                idInstitution = Guid.NewGuid(),
                id_user = uInfo.idUser,
                Name = institution.Name,
                Adress = institution.Adress,
                DateCreate = DateTime.Now,
                //Logo = institution.Logo,
            };

            foreach (Guid id in users) {
               tbUser user = db.tbUser.Find(id);
                newInstitution.tbUser.Add(user);
            }

            db.tbInstitution.Add(newInstitution);
            db.SaveChanges();
        }
    }
}