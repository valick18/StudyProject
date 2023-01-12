using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace StudyProject.Models.Core
{
    public class TaskResultBuilder
    {
        private Guid idUser;
        private StudyPlatformEntities db;
        public TaskResultBuilder(StudyPlatformEntities db) {
            this.db = db;
            this.idUser = new UserInfo(db).idUser;
        }

        public void Build(Guid idTask, Guid idTest, string answer, DateTime timeNow, int? Rate) {
            tbTaskResult result = new tbTaskResult()
            {
                idTaskResult = Guid.NewGuid(),
                id_task = idTask,
                id_user = idUser,
                id_test = idTest,
                Rate = Rate,
                UserAnswer = answer,
                TimeCreate = timeNow
            };
            db.tbTaskResult.Add(result);
            db.SaveChanges();
        }

    }
}