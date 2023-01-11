using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace StudyProject.Models.Core
{
    public class UserAnswer
    {
        public string Answer;
        public Guid idTest;
        public Guid idTask;
        public Guid? idTaskVariant;
    }
}